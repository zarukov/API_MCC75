using API_MCC75.Handler;
using API_MCC75.Repositories.Data;
using API_MCC75.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_MCC75.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly AccountRepository accountRepository;

    public AccountsController(AccountRepository accountRepository)
	{
        this.accountRepository = accountRepository;
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
            if(result == false)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Failed to Login."
                });
            }
            else
            {
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Succeed to Login."
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
