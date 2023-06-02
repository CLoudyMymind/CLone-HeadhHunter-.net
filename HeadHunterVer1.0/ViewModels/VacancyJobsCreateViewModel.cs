﻿namespace HeadHunterVer1._0.ViewModels;

public class VacancyJobsCreateViewModel
{
    public string Id { get; set; }

    public string UserId { get; set; }

    public string CategoryId { get; set; }

    public decimal Wages { get; set; }

    public bool IsPublished { get; set; }

    public List<CategoryViewModel>? CategoryViewModels { get; set; }

    public int? ExperienceYearsFrom { get; set; }
    public int? ExperienceYearsTo { get; set; }
    
    public string NameOfVacancy { get; set; }
}