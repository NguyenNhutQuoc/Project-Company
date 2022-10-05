using System.Data.SqlClient;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebAPIBiz4Company.DA;
using WebAPIBiz4Company.Interface.Job;
using WebAPIBiz4Company.Interface.JobType;
using WebAPIBiz4Company.Models;
using WebAPIBiz4Company.Models.Dto;

namespace WebAPIBiz4Company.Controllers;

[ApiController]
[Route("api/jobs")]
public class JobController : ControllerBase
{
    private readonly IJobActivity _jobActivity;

    public JobController(IJobActivity jobActivity)
    {
        _jobActivity = jobActivity;
    }
    
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_jobActivity.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        Job? job = _jobActivity.GetById(id);

        if (job is null)
        {
            return NotFound(new Error.Error
            {
                Status = HttpStatusCode.NotFound,
                Content = "NOT FOUND"
            });
        }

        return Ok(job);
    }

    [HttpGet("search")]
    public IActionResult GetBy([FromQuery] String? name, [FromQuery] String? address, [FromQuery] String? workingForm,
        [FromQuery] int type = 0)
    {
        List<SqlParameter> parameters = new List<SqlParameter>();
        parameters.Add(new SqlParameter("@name", name));
        parameters.Add(new SqlParameter("@address", address));
        parameters.Add(new SqlParameter("@workingForm", workingForm));
        parameters.Add(new SqlParameter("@type", type));
         
        List<Job> data = _jobActivity.GetBy(parameters);

        if (data.Count > 0)
        {
            return Ok(data);
        }

        return NotFound(new Error.Error
        {
            Status = HttpStatusCode.NotFound,
            Content = "NOT FOUND",
        });
    }

    [HttpPost]
    public IActionResult Post(JobDto jobDto)
    {
        Job? job = TransformDto.ToObject(jobDto, new Job());
        JobTypeActivity jobTypeActivity = new JobTypeActivity();
        JobType? jobType = jobTypeActivity.GetJobTypeById(jobDto.JobType);
        Console.WriteLine(jobType);
        if (jobType is not null)
        {
            Console.WriteLine("Helo");
            job.JobTypeNavigation = jobType;
        }
        
        job.JobDateCreated = DateTime.Now;
        string? notify = _jobActivity.Save(job);
        Console.WriteLine(notify);
        if (!notify.IsNullOrEmpty())
        {
            return BadRequest(new Error.Error
            {
                Status = HttpStatusCode.BadRequest,
                Content = notify
            });
        }
        return Ok(_jobActivity.GetAll());
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, JobDto jobDto)
    {
        Job? oldJob = _jobActivity.GetById(id);
        Job? job = TransformDto.ToObject(jobDto, oldJob);
        string? notify = _jobActivity.Update(id, job);
        if (oldJob is null)
        {
            NotFound(new Error.Error
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
        return Ok(_jobActivity.GetById(id));
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteById(int id)
    {
        Job? job = _jobActivity.GetById(id);
        string? notify = _jobActivity.DeleteById(id);
        if (job is null)
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

        return Ok(job);
    }
}