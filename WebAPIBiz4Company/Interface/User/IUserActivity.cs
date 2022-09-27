using System;
using System.Data.SqlClient;
using WebAPIBiz4Company.Models;
namespace WebAPIBiz4Company.Interface.User
{
    public interface IUserActivity
    {
        List<Models.User> GetAllUser();

        List<Models.User>? GetUsersBy(List<SqlParameter>? parameters);

        Models.User? GetUserById(int id);

        List<Models.User> CreateUser(Models.User user);

        List<Models.User> UpdateUser(Models.User user);

        string? DeleteUser(int id);
    }
}

