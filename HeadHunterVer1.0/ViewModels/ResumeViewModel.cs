using HeadHunterVer1._0.Models;
using HeadHunterVer1._0.Models.Employee;

namespace HeadHunterVer1._0.ViewModels;

public class ResumeViewModel
{
    public int Id { get; set; }
    public User Employee { get; set; }
    public string NameOfResume { get; set; }
    public int ExpectedSalary { get; set; }
    public string TelegramLink { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string? FacebookLink { get; set; }
    public string? LinkedInLink { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Category Category { get; set; }
    public List<WorkExperience>? WorkExperiences { get; set; }
    public List<Course>? Courses { get; set; }
}