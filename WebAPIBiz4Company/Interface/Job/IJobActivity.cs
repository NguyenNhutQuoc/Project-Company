using System.Data.SqlClient;

namespace WebAPIBiz4Company.Interface.Job;

public interface IJobActivity
{
    List<Models.Job> GetAll();

    Models.Job? GetById(int id);

    List<Models.Job> GetBy(List<SqlParameter> parameters);

    string? Save(Models.Job job);
    
    string? Update (int id, Models.Job? job);

    string? DeleteById(int id);
}