namespace WebAPIBiz4Company.Models.Dto;

public class UserDto
{
    public int UserId { get; set; }
    public string? UserFullname { get; set; }
    public string? UserEmail { get; set; }
    public string? UserPhoneNumber { get; set; }
    public string? UserCompanyName { get; set; }
    public string? UserQuestion { get; set; }
}