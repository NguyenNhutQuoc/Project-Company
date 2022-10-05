using System.Data.SqlClient;
using WebAPIBiz4Company.DA;

namespace WebAPIBiz4Company.Interface.Job;

public class JobActivity:IJobActivity
{
    private readonly string _connectionString =
        "Server=125.212.252.5;Database=WEB-PUBLIC-BETA;User Id=db.dev.tts.2022;Password=tVHcz%y!Z333";
    public List<Models.Job> GetAll()
    {
        return DbUtils.GetDataInTable<Models.Job>("spGetJobs", _connectionString); 
    }

    public Models.Job? GetById(int id)
    {
        List<SqlParameter> parameters = new List<SqlParameter>();
        parameters.Add(new SqlParameter("@id", id));
        List<Models.Job> data = DbUtils.GetDataInTable<Models.Job>("spGetJobs", _connectionString, parameters);
        if (data.Count > 0)
        {
            return data[0];
        }

        return null;
    }

    public List<Models.Job> GetBy(List<SqlParameter> parameters)
    {
        return DbUtils.GetDataInTable<Models.Job>("spGetJobs", _connectionString, parameters);
    }

    public string? Save(Models.Job job)
    {
        List<SqlParameter> parameters = new List<SqlParameter>();
        parameters.Add(new SqlParameter("@jobName", job.JobName));
        parameters.Add(new SqlParameter("@jobDescription", job.JobDescription));
        parameters.Add(new SqlParameter("@jobAddress", job.JobAddress));
        parameters.Add(new SqlParameter("@jobWorkingForm", job.JobWorkingForm));
        parameters.Add(new SqlParameter("@jobType", job.JobType));
        return DbUtils.RunSpToModifiedDataInTables("spCreateAJob", parameters, _connectionString);
    }

    public string? Update(int id, Models.Job? job)
    {
        List<SqlParameter> parameters = new List<SqlParameter>();
        parameters.Add(new SqlParameter("@jobId", id));
        parameters.Add(new SqlParameter("@jobName", job.JobName));
        parameters.Add(new SqlParameter("@jobDescription", job.JobDescription));
        parameters.Add(new SqlParameter("@jobAddress", job.JobAddress));
        parameters.Add(new SqlParameter("@jobWorkingForm", job.JobWorkingForm));
        parameters.Add(new SqlParameter("@jobType", job.JobType));
        return DbUtils.RunSpToModifiedDataInTables("spUpdateJob", parameters, _connectionString);
    }

    public string? DeleteById(int id)
    {
        List<SqlParameter> parameters = new List<SqlParameter>();
        parameters.Add(new SqlParameter("@id", id));
        return DbUtils.RunSpToModifiedDataInTables("spDeleteJob", parameters, _connectionString);
    }
}