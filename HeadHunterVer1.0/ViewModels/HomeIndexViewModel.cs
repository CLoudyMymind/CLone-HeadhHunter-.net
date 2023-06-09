using HeadHunterVer1._0.Extensions.Enums;
using HeadHunterVer1._0.Models.Employer;

namespace HeadHunterVer1._0.ViewModels;

public class HomeIndexViewModel
{
    public List<VacancyViewModel> VacancyViewModel { get; set; }

    public SortState PriceSort { get; set; }
    public SortState CategorySort { get; set; }
   
    public PaginationViewModel PaginationViewModel { get; set; }

    public List<ResumeViewModel> ResumeViewModels { get; set; }
}