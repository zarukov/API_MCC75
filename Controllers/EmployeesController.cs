using API_MCC75.Base;
using API_MCC75.Models;
using API_MCC75.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_MCC75.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : BaseController<string, Employee, EmployeeRepository>
{
	public EmployeesController(EmployeeRepository employeeRepository) : base(employeeRepository)
	{
	}
}
