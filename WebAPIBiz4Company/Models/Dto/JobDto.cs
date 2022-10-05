namespace WebAPIBiz4Company.Models.Dto;

public class JobDto
{
    public int JobId { get; set; }
    public string? JobName { get; set; }
    public string? JobDescription { get; set; }
    public string? JobAddress { get; set; }
    public string? JobWorkingForm { get; set; }
    public DateTime JobDateCreated { get; set; }
    public int JobType { get; set; }
    
    public DateTime? JobDateUpdated { get; set; }
}