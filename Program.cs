using API_MCC75.Repositories.Data;
using Microsoft.EntityFrameworkCore;
using API_MCC75.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Configure DbContext to Sql Server Database
var connectionString = builder.Configuration.GetConnectionString("Connection");
builder.Services.AddDbContext<MyContext>(options => options.UseSqlServer(connectionString));

//Dependency Injection
builder.Services.AddScoped<AccountRepository>();
builder.Services.AddScoped<AccountRoleRepository>();
builder.Services.AddScoped<EducationRepository>();
builder.Services.AddScoped<EmployeeRepository>();
builder.Services.AddScoped<ProfilingRepository>();
builder.Services.AddScoped<RoleRepository>();
builder.Services.AddScoped<UniversityRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;//tanya
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            //usually, this is app-base url
            ValidateAudience = false,
            /*ValidAudience = builder.Configuration["JWT : Audience"],--> kenapa di comment*/

            //if the JWT was created using web service, then this could be a consumer-base URL
            ValidateIssuer = false,
            /*ValidIssuer = builder.Configuration["JWT : Issuer"],--> kenapa di comment*/

            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

/*app.UseSession();
app.Use(async (context, next) =>
{
    var jwtoken = context.Session.GetString("jwtoken");
    if (!string.IsNullOrEmpty(jwtoken))
    {
        context.Request.Headers.Add("Authorization", "Bearer " + jwtoken);
    }
    await next();
});*/

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
