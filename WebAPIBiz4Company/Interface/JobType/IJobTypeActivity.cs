using System.Data.SqlClient;

namespace WebAPIBiz4Company.Interface.JobType;

public interface IJobTypeActivity
{
    public List<Models.JobType> GetAllJobTypes();
    
    public List<Models.JobType> GetJobTypesBy(List<SqlParameter> parameters);

    public Models.JobType? GetJobTypeById(int id);

    public string? CreateJobType(Models.JobType jobType);
    
    public string? UpdateJobType(int id, Models.JobType? jobType);
    
    public string? DeleteJobType(int id);
    
    
    
}