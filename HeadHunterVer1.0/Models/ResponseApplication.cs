using HeadHunterVer1._0.Models.Employee;
using HeadHunterVer1._0.Models.Employer;
using HeadHunterVer1._0.ViewModels;

namespace HeadHunterVer1._0.Models;

public class ResponseApplication
{
    public string Id { get; set; }

    public Resume? Resume { get; set; }

    public int ResumeId { get; set; }

    public Vacancy? Vacancy { get; set; }

    public string VacancyId { get; set; }

    public User? User { get; set; }

    public string UserId { get; set; }

    public DateTime DispatchTime { get; set; }

    public bool? IsAcceptOrRejectedResponse { get; set; }

}