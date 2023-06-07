namespace HeadHunterVer1._0.ViewModels;

public class EditVacancyViewModel
{
    public string? Id { get; set; }
    public string? CategoryId { get; set; }

    public decimal? Wages { get; set; }
    public DateTime? UpdateVacancyBid { get; set; }

    public bool? IsPublished { get; set; }
    public string? Description { get; set; }
    public string? SelectedCategoryName { get; set; }
    public string? Caption { get; set; }
    public List<CategoryViewModel>? CategoryViewModels { get; set; }

    public int? ExperienceYearsFrom { get; set; }
    public int? ExperienceYearsTo { get; set; }
    
}