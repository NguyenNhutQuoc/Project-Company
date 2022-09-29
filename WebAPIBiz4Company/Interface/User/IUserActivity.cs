using System;
using System.Data.SqlClient;
using WebAPIBiz4Company.Models;
using WebAPIBiz4Company.Models.Dto;

namespace WebAPIBiz4Company.Interface.User
{
    public interface IUserActivity
    {
        List<Models.User> GetAllUser();

        List<Models.User>? GetUsersBy(List<SqlParameter>? parameters);

        Models.User? GetUserById(int id);

        string? CreateUser(Models.User user);

        string? UpdateUser(Models.User user);

        string? DeleteUser(int id);
    }
}

