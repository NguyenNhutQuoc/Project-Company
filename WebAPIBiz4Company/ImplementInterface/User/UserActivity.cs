using System;
using Microsoft.EntityFrameworkCore.Metadata;
using WebAPIBiz4Company.DA;
using WebAPIBiz4Company.Interface.User;

namespace WebAPIBiz4Company.ImplementInterface.User
{
    public class UserActivity:IUserActivity
    {
        private string connectionString =
            "Server=125.212.252.5;Database=WEB-PUBLIC-BETA;User Id=db.dev.tts.2022;Password=tVHcz%y!Z333";
        List<Models.User> IUserActivity.CreateUser(Models.User user)
        {
            throw new NotImplementedException();
        }

        List<Models.User> IUserActivity.DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        List<Models.User> IUserActivity.GetAllUser()
        {
            return DbUtils.GetAllDataInTable("spGetAllUser", connectionString);
        }

        Models.User IUserActivity.GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        List<Models.User> IUserActivity.UpdateUser(Models.User user)
        {
            throw new NotImplementedException();
        }
    }
}

