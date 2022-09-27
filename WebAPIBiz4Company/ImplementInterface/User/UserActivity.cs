using System.Data;
using System.Data.SqlClient;
using WebAPIBiz4Company.DA;
using WebAPIBiz4Company.Interface.User;

namespace WebAPIBiz4Company.ImplementInterface.User
{
    public class UserActivity:IUserActivity
    {
        private readonly string _connectionString =
            "Server=125.212.252.5;Database=WEB-PUBLIC-BETA;User Id=db.dev.tts.2022;Password=tVHcz%y!Z333";
        List<Models.User> IUserActivity.CreateUser(Models.User user)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@userFullname", user.UserFullname));
            parameters.Add(new SqlParameter("@userEmail", user.UserEmail));
            parameters.Add(new SqlParameter("@userPhoneNumber", user.UserPhoneNumber));
            parameters.Add(new SqlParameter("@userCompanyName", user.UserCompanyName));
            parameters.Add(new SqlParameter("@userQuestion", user.UserQuestion));
            string? notify = DbUtils.RunSpToModifiedDataInTables("spCreateUser", parameters, _connectionString);
            if (notify is not null && notify.Equals("Created Successfully"))
            {
                return DbUtils.GetDataInTable<Models.User>("spGetAllUser", _connectionString);
            }

            return new List<Models.User>();
        }

        string? IUserActivity.DeleteUser(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@id", id));
            string? notify = DbUtils.RunSpToModifiedDataInTables("spDeleteUser", parameters, _connectionString);
            if (notify is not null )
            {
                notify = "Deleted Successfully";
            }
            return notify;
        }

        List<Models.User> IUserActivity.GetAllUser()
        {
            return DbUtils.GetDataInTable<Models.User>("spGetUsers", _connectionString);
        }

        public List<Models.User>? GetUsersBy(List<SqlParameter>? parameters)
        {
            return DbUtils.GetDataInTable<Models.User>("spGetUsers", _connectionString, parameters);
        }

        Models.User? IUserActivity.GetUserById(int id)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@id", id));
            list.Add(new SqlParameter
            {
                Direction = ParameterDirection.Output,
                SqlDbType = SqlDbType.NVarChar,
                ParameterName = "@notify",
                Size = 255
            });
            List<Models.User> data = DbUtils.GetDataInTable<Models.User>("spGetUsers", _connectionString, list);
            SqlParameter notify = new SqlParameter();
            foreach (var parameter in list)
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

            return data[0];
        }

        List<Models.User> IUserActivity.UpdateUser(Models.User user)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@userId", user.UserId));
            parameters.Add(new SqlParameter("@userFullname", user.UserFullname));
            parameters.Add(new SqlParameter("@userEmail", user.UserEmail));
            parameters.Add(new SqlParameter("@userPhoneNumber", user.UserPhoneNumber));
            parameters.Add(new SqlParameter("@userCompanyName", user.UserCompanyName));
            parameters.Add(new SqlParameter("@userQuestion", user.UserQuestion));
            string? notify = DbUtils.RunSpToModifiedDataInTables("spUpdateUser", parameters, _connectionString);
            if (notify is not null && notify.Equals("Created Successfully"))
            {
                return DbUtils.GetDataInTable<Models.User>("spGetAllUser", _connectionString);
            }

            return new List<Models.User>();
        }
    }
}

