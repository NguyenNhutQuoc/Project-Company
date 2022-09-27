using System.Data;
using Microsoft.AspNetCore.Mvc;
using WebAPIBiz4Company.Interface.User;
using WebAPIBiz4Company.Models;
using System.Web;
using System.Data.SqlClient;

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
    public IEnumerable<User> Get()
    {
        List<User> users = _userActivity.GetAllUser();
        return users;
    }

    [HttpGet("{id}")]
    public User? GetById(int id)
    {
        return _userActivity.GetUserById(id);
    }

    [HttpGet("search")]
    public IEnumerable<User>? GetBy([FromQuery] String? name=null,
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
        if (data.Count == 0 && notify.Value.Equals("NOT FOUND"))
        {
            return null;
        }

        return data;
    }

    [HttpPost]
    public IEnumerable<User> Post(User user)
    {
        List<User> users = _userActivity.CreateUser(user);
        return users;
    }

    [HttpPut("{id}")]
    public IEnumerable<User> Put(int id, User user)
    {
        user.UserId = id;
        List<User> users = _userActivity.UpdateUser(user);
        return users;
    }

    [HttpDelete("{id}")]
    public string? Delete(int id)
    {
        return _userActivity.DeleteUser(id);
    }
}

