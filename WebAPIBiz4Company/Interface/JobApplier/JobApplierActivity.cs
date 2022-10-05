using System.Data.SqlClient;
using WebAPIBiz4Company.DA;

namespace WebAPIBiz4Company.Interface.JobApplier;

public class JobApplierActivity : IJopApplierActivity
{
    private readonly string _connectionString = "Server=125.212.252.5;Database=WEB-PUBLIC-BETA;User Id=db.dev.tts.2022;Password=tVHcz%y!Z333";
    public List<Models.JobApplier> GetAll()
    {
        return DbUtils.GetDataInTable<Models.JobApplier>("spGetJobApplier", _connectionString);
    }

    public Models.JobApplier? GetById(int id)
    {
        List<SqlParameter> parameters = new List<SqlParameter>();
        parameters.Add(new SqlParameter("id", id));
        List<Models.JobApplier> data = DbUtils.GetDataInTable<Models.JobApplier>("spGetJobApplier", _connectionString, parameters);
        if (data.Count > 0)
        {
            return data[0];
        }
        return null;
    }

    public List<Models.JobApplier> GetBy(List<SqlParameter> parameters)
    {
        return DbUtils.GetDataInTable<Models.JobApplier>("spGetJobApplier", _connectionString, parameters);
    }

    public string? Save(Models.JobApplier jobApplier, int jobId)
    {
        List<SqlParameter> parameters = new List<SqlParameter>();
        parameters.Add(new SqlParameter("@jobApplierFullname", jobApplier.JobApplierFullname));
        parameters.Add(new SqlParameter("@jobApplierEmail", jobApplier.JobApplierEmail));
        parameters.Add(new SqlParameter("@jobApplierPhoneNumber", jobApplier.JobApplierPhoneNumber));
        parameters.Add(new SqlParameter("@jobApplierAddress", jobApplier.JobApplierAddress));
        parameters.Add(new SqlParameter("@jobApplierPresentCompany", jobApplier.JobApplierPresentCompany));
        parameters.Add(new SqlParameter("@jobApplierExperience", jobApplier.JobApplierExperience));
        parameters.Add(new SqlParameter("@jobApplierCV", jobApplier.JobApplierCv));
        parameters.Add(new SqlParameter("@jobApplierBeInformed", jobApplier.JobApplierBeInformed));
        parameters.Add(new SqlParameter("@jobId", jobId));
        return DbUtils.RunSpToModifiedDataInTables("spCreateAJobApplier", parameters, _connectionString);
    }

    public string? Update(int id, Models.JobApplier? jobApplier)
    {
        List<SqlParameter> parameters = new List<SqlParameter>();
        parameters.Add(new SqlParameter("@jobApplierId", id));
        parameters.Add(new SqlParameter("@jobApplierFullname", jobApplier.JobApplierFullname));
        parameters.Add(new SqlParameter("@jobApplierEmail", jobApplier.JobApplierEmail));
        parameters.Add(new SqlParameter("@jobApplierPhoneNumber", jobApplier.JobApplierPhoneNumber));
        parameters.Add(new SqlParameter("@jobApplierAddress", jobApplier.JobApplierAddress));
        parameters.Add(new SqlParameter("@jobApplierPresentCompany", jobApplier.JobApplierPresentCompany));
        parameters.Add(new SqlParameter("@jobApplierExperience", jobApplier.JobApplierExperience));
        parameters.Add(new SqlParameter("@jobApplierCV", jobApplier.JobApplierCv));
        parameters.Add(new SqlParameter("@jobApplierBeInformed", jobApplier.JobApplierBeInformed));
        return DbUtils.RunSpToModifiedDataInTables("spUpdateJobApplier", parameters, _connectionString);
    }

    public string? DeleteById(int id)
    {
        List<SqlParameter> parameters = new List<SqlParameter>();
        parameters.Add(new SqlParameter("@id", id));
        return DbUtils.RunSpToModifiedDataInTables("spDeleteJobApplier", parameters, _connectionString);
    }
}