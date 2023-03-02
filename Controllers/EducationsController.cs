using API_MCC75.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_MCC75.Models;
using API_MCC75.Base;

namespace API_MCC75.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EducationsController : BaseController<int, Education, EducationRepository>
{
	public EducationsController(EducationRepository educationRepository) : base(educationRepository)
	{

	}
}
