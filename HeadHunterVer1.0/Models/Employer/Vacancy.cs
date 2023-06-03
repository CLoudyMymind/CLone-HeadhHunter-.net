namespace HeadHunterVer1._0.Models.Employer;

public class Vacancy
{
    public string Id { get; set; }

    public string UserId { get; set; }

    public User? User { get; set; }

    public decimal Wages { get; set; }

    public bool IsPublished { get; set; }

    public Category? Category { get; set; }

    public string? CategoryId { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }
    public int? ExperienceYearsFrom { get; set; }
    public int? ExperienceYearsTo { get; set; }

    public DateTime UpdateVacancyBid { get; set; }
}