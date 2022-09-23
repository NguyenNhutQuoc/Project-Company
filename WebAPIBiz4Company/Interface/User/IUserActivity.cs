using System;
using WebAPIBiz4Company.Models;
namespace WebAPIBiz4Company.Interface.User
{
    public interface IUserActivity
    {
        List<Models.User> GetAllUser();

        Models.User GetUserById(int id);

        List<Models.User> CreateUser(Models.User user);

        List<Models.User> UpdateUser(Models.User user);

        List<Models.User> DeleteUser(int id);
    }
}

