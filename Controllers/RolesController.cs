using API_MCC75.Base;
using API_MCC75.Models;
using API_MCC75.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_MCC75.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController : BaseController<int, Role, RoleRepository>
{
	public RolesController(RoleRepository roleRepository) : base(roleRepository)
	{

	}
}
