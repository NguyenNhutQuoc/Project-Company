using System.Data.SqlClient;

namespace WebAPIBiz4Company.Interface.JobApplier;

public interface IJopApplierActivity
{
    List<Models.JobApplier> GetAll();

    Models.JobApplier? GetById(int id);

    List<Models.JobApplier> GetBy(List<SqlParameter> parameters);

    string? Save(Models.JobApplier jobApplier, int jobId);
    
    string? Update(int id, Models.JobApplier? jobApplier);

    string? DeleteById(int id);
}