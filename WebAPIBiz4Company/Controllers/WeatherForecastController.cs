using Microsoft.AspNetCore.Mvc;
using WebAPIBiz4Company.Interface.User;
using WebAPIBiz4Company.Models;

namespace WebAPIBiz4Company.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private IUserActivity _userActivity;
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IUserActivity userActivity)
    {
        _logger = logger;
        _userActivity = userActivity;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<User> Get()
    {
        List<User> users = _userActivity.GetAllUser();
        return users.GetRange(0, users.Count).ToArray();
    }
}

