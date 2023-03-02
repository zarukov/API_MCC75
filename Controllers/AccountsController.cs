using API_MCC75.Handler;
using API_MCC75.Repositories.Data;
using API_MCC75.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_MCC75.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly AccountRepository accountRepository;
    private readonly IConfiguration configuration;

    public AccountsController(AccountRepository accountRepository, IConfiguration configuration)
	{
        this.accountRepository = accountRepository;
        this.configuration = configuration;
    }

    [HttpPost("/Register")]
    public async Task<ActionResult> Register(RegisterVM registerVM)
    {
        try
        {
            var result = accountRepository.Register(registerVM);
            if(result == null)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Failed to Create a New Data."
                });
            }
            else
            {
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Succeed to Create a New Data.",
                    Data = result
                });
            }
        }
        catch
        {
            return BadRequest(new
            {
                StatusCode = 500,
                Message = "Something Wrong. Please Try Again."
            });
        }
    }
    [HttpPost("/Login")]
    public async Task<ActionResult> Login(LoginVM loginVM)
    {
        try
        {
            var result = await accountRepository.Login(loginVM);
            if (result == false)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Failed to Login."
                });
            }
            else
            {
                var userdata = await accountRepository.GetUserdata(loginVM.Email);
                var roles = await accountRepository.GetRolesByNIK(loginVM.Email);
                var claims = new List<Claim>()
                {
                    new Claim (ClaimTypes.Email, userdata.Email),
                    new Claim (ClaimTypes.Name, userdata.FullName)
                };
                foreach (var item in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item));
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: configuration["JWT:Issuer"],
                    audience: configuration["JWT:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signIn
                    );

                var generateToken = new JwtSecurityTokenHandler().WriteToken(token);
               
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Succeed to Login.",
                    Data = generateToken
                });
            }

        }
        catch
        {
            return BadRequest(new
            {
                StatusCode = 400,
                Message = "Something Error."
            });
        }
    }

}
