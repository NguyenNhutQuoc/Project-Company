using System.Data;
using System.Data.SqlClient;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebAPIBiz4Company.Interface.JobType;
using WebAPIBiz4Company.Models;
using WebAPIBiz4Company.Models.Dto;

namespace WebAPIBiz4Company.Controllers;

[ApiController]
[Route("api/job-type")]
public class JobTypeController : ControllerBase
{
    private readonly IJobTypeActivity _jobTypeActivity;

    public JobTypeController(IJobTypeActivity jobTypeActivity)
    {
        _jobTypeActivity = jobTypeActivity;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_jobTypeActivity.GetAllJobTypes());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        JobType? jobType = _jobTypeActivity.GetJobTypeById(id);
        if (jobType is null)
        {
            return NotFound(
                    new Error.Error
                    {
                        Status = HttpStatusCode.NotFound,
                        Content = "NOT FOUND"
                    });
        }

        return Ok(jobType);
    }

    [HttpGet("search")]
    public IActionResult Search([FromQuery] string? name = null)
    {
        List<SqlParameter> parameters = new List<SqlParameter>();
        parameters.Add(new SqlParameter("@name", name));
        parameters.Add(new SqlParameter
        {
            Direction = ParameterDirection.Output,
            ParameterName = "@notify",
            SqlDbType = SqlDbType.NVarChar,
            Size = 255
        });
        List<JobType> data = _jobTypeActivity.GetJobTypesBy(parameters);
        string notify = "";
        foreach (var param in parameters)
        {
            if (param.Direction is ParameterDirection.Output)
            {
                notify = param.ParameterName;
                break;
            }
        }
        if (data.Count == 0)
        {
            return NotFound(new Error.Error
            {
                Status = HttpStatusCode.NotFound,
                Content = notify
            });
        }
        return Ok(data);
    }

    [HttpPost]
    public IActionResult Post(JobType jobType)
    {
        string? notify = _jobTypeActivity.CreateJobType(jobType);
        if (!notify.IsNullOrEmpty())
        {
            return BadRequest(new Error.Error
            {
                Status = HttpStatusCode.NotFound,
                Content = notify
            });
        }

        return GetAll();
    }
    
    [HttpPut("{id}")]
    public IActionResult Put(int id, JobTypeDto jobTypeDto)
    {
        jobTypeDto.JobTypeId = id;
        JobType? oldJobType = _jobTypeActivity.GetJobTypeById(id);
        JobType? newJobType = TransformDto.ToObject(jobTypeDto, oldJobType);
        string? notify = _jobTypeActivity.UpdateJobType(id, newJobType);
        if (oldJobType is null)
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
        return Ok(_jobTypeActivity.GetJobTypeById(id));
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        JobType? jobType = _jobTypeActivity.GetJobTypeById(id);
        string? notify = _jobTypeActivity.DeleteJobType(id);
        if (jobType is null)
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
        return Ok(jobType);
    }
}