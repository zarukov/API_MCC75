using API_MCC75.Contexts;
using API_MCC75.Handler;
using API_MCC75.Models;
using API_MCC75.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace API_MCC75.Repositories.Data;

public class AccountRepository : GeneralRepository<int, Account>
{
    private readonly MyContext context;

    public AccountRepository(MyContext context) : base(context)
	{
        this.context = context;
    }
    public async Task<int> Register(RegisterVM registerVM)
    {
        int result = 0;
        // Bikin kondisi untuk mengecek apakah data university sudah ada
        University university = new University
        {
            Name = registerVM.UniversityName
        };
        if (context.Universities.Any(u => u.Name == registerVM.UniversityName))
        {
            university.Id = context.Universities.
                SingleOrDefaultAsync(u => u.Name == university.Name).
                Id;
        }
        else
        {
            context.Universities.AddAsync(university);
            await context.SaveChangesAsync();
        }

        Education education = new Education
        {
            Major = registerVM.Major,
            Degree = registerVM.Degree,
            GPA = registerVM.GPA,
            UniversityId = university.Id
        };
        context.Educations.AddAsync(education);
        await context.SaveChangesAsync();

        Employee employee = new Employee
        {
            NIK = registerVM.NIK,
            FirstName = registerVM.FirstName,
            LastName = registerVM.LastName,
            BirthDate = registerVM.BirthDate,
            Gender = (API_MCC75.Models.GenderEnum)registerVM.Gender,
            HiringDate = registerVM.HiringDate,
            Email = registerVM.Email,
            PhoneNumber = registerVM.PhoneNumber,
        };
        context.Employees.AddAsync(employee);
        await context.SaveChangesAsync();

        Account account = new Account
        {
            EmployeeNIK = registerVM.NIK,
            Password = Hashing.HashPassword(registerVM.Password),
        };
        context.Accounts.AddAsync(account);
        await context.SaveChangesAsync();

        AccountRole accountRole = new AccountRole
        {
            AccountNIK = registerVM.NIK,
            RoleId = 2
        };

        context.AccountRoles.Add(accountRole);
        await context.SaveChangesAsync();

        Profiling profiling = new Profiling
        {
            EmployeeNIK = registerVM.NIK,
            EducationId = education.Id
        };
        context.Profilings.Add(profiling);
        await context.SaveChangesAsync();

        return result;
    }
    public async Task<bool> Login(LoginVM loginVM)
    {

        var result = await context.Employees
            .Include(e => e.Account)
            .Select(e => new LoginVM
            {
                Email = e.Email,
                Password = e.Account.Password
            }).SingleOrDefaultAsync(a => a.Email == loginVM.Email);
        if (result == null)
        {
            return false;
        }
        return Hashing.ValidatePassword(loginVM.Password, result.Password);
    }
}
