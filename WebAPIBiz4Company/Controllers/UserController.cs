using System.Data;
using Microsoft.AspNetCore.Mvc;
using WebAPIBiz4Company.Interface.User;
using WebAPIBiz4Company.Models;
using System.Data.SqlClient;
using System.Net;
using WebAPIBiz4Company.Models.Dto;
using Microsoft.IdentityModel.Tokens;
using WebAPIBiz4Company.Models.Dto;

namespace WebAPIBiz4Company.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserActivity _userActivity;

    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger, IUserActivity userActivity)
    {
        _logger = logger;
        _userActivity = userActivity;
    }

    [HttpGet]
    public IActionResult Get()
    {
        List<User> users = _userActivity.GetAllUser();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    { 
        User? user = _userActivity.GetUserById(id);
        if (user is null)
        {
            return NotFound(new Error.Error
            {
                Status = HttpStatusCode.NotFound,
                Content = "NOT FOUND"
            });
        }

        return Ok(user);
    }

    [HttpGet("search")]
    public IActionResult GetBy([FromQuery] String? name=null,
        [FromQuery] String? email =null,[FromQuery] String? phoneNumber=null,
        [FromQuery] String? companyName=null)
    {
        List<SqlParameter>? parameters = new List<SqlParameter>();
        parameters.Add(new SqlParameter("@fullname", name));
        parameters.Add(new SqlParameter("@email", email));
        parameters.Add(new SqlParameter("@phoneNumber", phoneNumber));
        parameters.Add(new SqlParameter("@companyName", companyName));
        parameters.Add(new SqlParameter
        {
            Direction = ParameterDirection.Output,
            SqlDbType = SqlDbType.NVarChar,
            ParameterName = "@notify",
            Size = 255
        });
        List<User>? data = _userActivity.GetUsersBy(parameters);
        SqlParameter notify = new SqlParameter();
        foreach (var parameter in parameters)
        {
            if (parameter.ParameterName.Equals("@notify"))
            {
                notify = parameter;
                break;
            }
        }
        if (data.Count == 0 && !notify.IsNullable)
        {
            if (notify.Value.ToString().Equals("NOT FOUND"))
            {
                return NotFound(new Error.Error
                {
                    Status = HttpStatusCode.NotFound,
                    Content = notify.Value.ToString()
                });
            }
            else
            {
                return BadRequest(new Error.Error
                {
                    Status = HttpStatusCode.BadRequest,
                    Content = notify.Value.ToString()
                });
            }
        }

        return Ok(data);
    }

    [HttpPost]
    public IActionResult Post(User user)
    {
        try
        {
            string? notify = _userActivity.CreateUser(user);
            if (!notify.IsNullOrEmpty())
            {
                return BadRequest(new Error.Error
                {
                    Status = HttpStatusCode.BadRequest,
                    Content = notify
                });
            }

            return Get();
        }
        catch (Exception e)
        {
            return CreatedAtRoute(HttpStatusCode.InternalServerError,
                e.Message
            );
        }
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, UserDto userDto)
    {
        userDto.UserId = id;
        User? oldUser = _userActivity.GetUserById(id);
        if (oldUser is null)
        {
            return NotFound(new Error.Error
            {
                Status = HttpStatusCode.NotFound,
                Content = "Not found any user with id: "+id
            });
        }
        //
        // User user = new User();
        // int index = 0;
        // foreach (PropertyInfo property in userDto.GetType().GetProperties())
        // {
        //     if (property.GetValue(userDto) is null)
        //     {
        //         PropertyInfo properOldUser = oldUser.GetType().GetProperties()[index];
        //         user.GetType().GetProperties()[index].SetValue(user, properOldUser.GetValue(oldUser));
        //     }
        //     else
        //     {
        //         user.GetType().GetProperties()[index].SetValue(user, property.GetValue(userDto));
        //     }
        //     index++;
        // }
        User user = TransformDto.ToObject(userDto, oldUser);
        string? notify = _userActivity.UpdateUser(user);
        Console.WriteLine(notify);
        if (notify != "")
        {
            return BadRequest(
                new Error.Error
                {
                    Status = HttpStatusCode.BadRequest,
                    Content = notify
                });
        }
        return Ok(user);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        User? user = _userActivity.GetUserById(id);
        string? notify = _userActivity.DeleteUser(id);
        if (notify.IsNullOrEmpty())
        {
            return Get();
        }

        if (user is null)
        {
            return NotFound(new Error.Error
            {
                Status = HttpStatusCode.NotFound,
                Content = notify
            });
        }

        return BadRequest(new Error.Error
        {
            Status = HttpStatusCode.BadRequest,
            Content = notify
        });
    }
}

