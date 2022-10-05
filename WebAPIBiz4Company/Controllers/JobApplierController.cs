using System.Data.SqlClient;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebAPIBiz4Company.Interface.JobApplier;
using WebAPIBiz4Company.Models;
using WebAPIBiz4Company.Models.Dto;

namespace WebAPIBiz4Company.Controllers;

[ApiController]
[Route("api/job-appliers")]
public class JobApplierController:ControllerBase
{
    private readonly IJopApplierActivity _jobApplierActivity;

    public JobApplierController(IJopApplierActivity jobApplierActivity)
    {
        _jobApplierActivity = jobApplierActivity;
    }

    [HttpGet]
    public IActionResult Get()  
    {
        return Ok(_jobApplierActivity.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        Models.JobApplier? jobApplier = _jobApplierActivity.GetById(id);
        if (jobApplier is null)
        {
            return NotFound(new Error.Error
            {
                Status = HttpStatusCode.NotFound,
                Content = $"Not found any applier with the id: {id}"
            });
        }

        return Ok(jobApplier);
    }

    [HttpGet("search")]
    public IActionResult Search([FromQuery] String? name = null, [FromQuery] String? email = null,
        [FromQuery] String? phoneNumber = null, [FromQuery] String? address = null,
        [FromQuery] String? presentCompany = null, [FromQuery] int experience = -1, [FromQuery] int   beInformed= -1,
        [FromQuery] int jobApplyJob = 0)    
    {
        List<SqlParameter> parameters = new List<SqlParameter>();
        parameters.Add(new SqlParameter("@fullname", name));
        parameters.Add(new SqlParameter("@email", email));
        parameters.Add(new SqlParameter("@phoneNumber", phoneNumber));
        parameters.Add(new SqlParameter("@address", address));
        parameters.Add(new SqlParameter("@presentCompany", presentCompany));
        parameters.Add(new SqlParameter("@experience", experience));
        parameters.Add(new SqlParameter("@beInformed", beInformed));
        
        List<Models.JobApplier> data = _jobApplierActivity.GetBy(parameters);
        if (data.Count > 0)
        {
            return Ok(data);
        }

        return NotFound(new Error.Error
        {
            Status = HttpStatusCode.NotFound,
            Content = "NOT FOUND"
        });
    }

    [HttpPost]
    public IActionResult Post(JobApplier jobApplier)
    {
        string? notify = _jobApplierActivity.Save(jobApplier, 8);

        if (!notify.IsNullOrEmpty())
        {
            return BadRequest(new Error.Error
            {
                Status = HttpStatusCode.BadRequest,
                Content = notify
            });
        }
        
        return Ok(_jobApplierActivity.GetAll());
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, JobApplierDto jobApplierDto)
    {
        JobApplier? oldJobApplier = _jobApplierActivity.GetById(id);
        JobApplier? newJobApplier = TransformDto.ToObject(jobApplierDto, oldJobApplier);
        
        string? notify = _jobApplierActivity.Update(id, newJobApplier);
        if (oldJobApplier is null)
        {
            return NotFound(new Error.Error
            {
                Status = HttpStatusCode.NotFound,
                Content = notify
            });
        }

        if (!notify.IsNullOrEmpty())
        {
            return BadRequest(new Error.Error
            {
                Status = HttpStatusCode.BadRequest,
                Content = notify
            });
        }

        return Ok(_jobApplierActivity.GetById(id));
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        JobApplier? jobApplier = _jobApplierActivity.GetById(id);
        string? notify = _jobApplierActivity.DeleteById(id);
        if (jobApplier is null)
        {
            return NotFound(new Error.Error
            {
                Status = HttpStatusCode.NotFound,
                Content = notify
            });
        }
        if (!string.IsNullOrEmpty(notify))
        {
            return BadRequest(new Error.Error
            {
                Status = HttpStatusCode.BadRequest,
                Content = notify
            });
        }

        return Ok(jobApplier);
    }
}