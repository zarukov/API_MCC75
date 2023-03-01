﻿using API_MCC75.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_MCC75.Models;

namespace API_MCC75.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EducationsController : ControllerBase
{
    private readonly EducationRepository educationRepository;

    public EducationsController(EducationRepository educationRepository)
	{
        this.educationRepository = educationRepository;
    }
    [HttpPost]
    public async Task<ActionResult> Insert(Education entity)
    {
        try
        {
            var result = await educationRepository.Insert(entity);
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

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var result = await educationRepository.GetAll();
        if (result == null)
        {
            return Ok(new
            {
                StatusCode = 200,
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

    [HttpPut]
    public async Task<ActionResult> Update(Education entity)
    {
        try
        {
            var result = await educationRepository.Update(entity);
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

    [HttpDelete]
    public async Task<ActionResult> Delete(int key)
    {
        try
        {
            var result = await educationRepository.Delete(key);
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

    [HttpGet]
    [Route("{key}")]
    public async Task<ActionResult> GetById(int key)
    {
        var result = await educationRepository.GetById(key);
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
}
