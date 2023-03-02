using API_MCC75.Models;
using API_MCC75.Repositories.Data;
using API_MCC75.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API_MCC75.Base;

[Route("api/[controller]")]
[ApiController]
public class BaseController<Key, Entity, Repository> : ControllerBase
    where Entity : class
    where Repository : IRepository<Key, Entity>
{
    private readonly Repository repository;

    public BaseController(Repository repository)
    {
        this.repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var result = await repository.GetAll();
        if (result == null)
        {
            return Ok(new
            {
                StatusCode = 400,
                Message = "Data isn't Available."
            });
        }
        else
        {
            return Ok(new
            {
                StatusCode = 200,
                Message = "Data is Available.",
                Data = result
            });
        }
    }
    [HttpGet]
    [Route("{key}")]
    public async Task<ActionResult> GetById(Key key)
    {
        var result = await repository.GetById(key);
        if (result == null)
        {
            return BadRequest(new
            {
                StatusCode = 400,
                Message = "Data Not Founded."
            });
        }
        else
        {
            return Ok(new
            {
                StatusCode = 200,
                Message = "Data Founded.",
                Data = result
            });
        }
    }
    [HttpPost]
    public async Task<ActionResult> Insert(Entity entity)
    {
        try
        {
            var result = await repository.Insert(entity);
            if (result == 0)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Data Failed to Save."
                });
            }
            else
            {
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Data Succeed to Save."
                });
            }
        }
        catch
        {
            return BadRequest(new
            {
                StatusCode = 500,
                Message = "Something Wrong! Try Again."
            });
        }

    }
    [HttpDelete]
    public async Task<ActionResult> Delete(Key key)
    {
        try
        {
            var result = await repository.Delete(key);
            if (result == null)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Data Wasn't Found. Failed to Delete.."
                });
            }
            else
            {
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Data Was Succeed to Delete."
                });
            }
        }
        catch
        {
            return BadRequest(new
            {
                StatusCode = 500,
                Message = "Something Wrong! Try Again."
            });
        }
    }
    [HttpPut]
    public async Task<ActionResult> Update(Entity entity)
    {
        try
        {
            var result = await repository.Update(entity);
            if (result == null)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Data is Failed to Update."
                });
            }
            else
            {
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Data is Succeed to Update."
                });
            }
        }
        catch
        {
            return BadRequest(new
            {
                StatusCode = 500,
                Message = "Something Wrong! Try Again."
            });
        }

    }
}
