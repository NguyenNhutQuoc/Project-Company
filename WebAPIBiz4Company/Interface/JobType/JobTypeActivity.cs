using System.Data;
using System.Data.SqlClient;
using WebAPIBiz4Company.DA;
using WebAPIBiz4Company.Interface.JobType;

namespace WebAPIBiz4Company.Interface.JobType;

public class JobTypeActivity : IJobTypeActivity
{
    private readonly string _connectionString =
        "Server=125.212.252.5;Database=WEB-PUBLIC-BETA;User Id=db.dev.tts.2022;Password=tVHcz%y!Z333";
    public List<Models.JobType> GetAllJobTypes()
    {
        return DbUtils.GetDataInTable<Models.JobType>("spGetJobsType", _connectionString);
    }

    public List<Models.JobType> GetJobTypesBy(List<SqlParameter> parameters)
    {
        return DbUtils.GetDataInTable<Models.JobType>("spGetJobsType", _connectionString, parameters);
    }

    public Models.JobType? GetJobTypeById(int id)
    {
        List<SqlParameter>? parameters = new List<SqlParameter>();
        parameters.Add(new SqlParameter("@id", id));
        parameters.Add(new SqlParameter
        {
            Direction = ParameterDirection.Output,
            ParameterName = "@notify",
            SqlDbType = SqlDbType.NVarChar,
            Size = 255
        });
        List<Models.JobType> data =
            DbUtils.GetDataInTable<Models.JobType>("spGetJobsType", _connectionString, parameters);
        if (data.Count == 0)
        {
            return null;
        }

        return data[0];
    }

    public string? CreateJobType(Models.JobType jobType)
    {
        List<SqlParameter> parameters = new List<SqlParameter>();
        
        parameters.Add(new SqlParameter("@jobTypeName", jobType.JobTypeName));
        
        return DbUtils.RunSpToModifiedDataInTables("spCreateAJobType", parameters, _connectionString);
    }

    public string? UpdateJobType(int id, Models.JobType? jobType)
    {
        List<SqlParameter> parameters = new List<SqlParameter>();
        if (jobType != null)
        {
            parameters.Add(new SqlParameter("@jobTypeId", jobType.JobTypeId));
            parameters.Add(new SqlParameter("@jobTypeName", jobType.JobTypeName));
        }
        
        return DbUtils.RunSpToModifiedDataInTables("spUpdateJobType", parameters, _connectionString);
    }

    public string? DeleteJobType(int id)
    {
        List<SqlParameter> parameters = new List<SqlParameter>();
        
        parameters.Add(new SqlParameter("@id", id));
        
        return DbUtils.RunSpToModifiedDataInTables("spDeleteJobType", parameters, _connectionString);
    }
}