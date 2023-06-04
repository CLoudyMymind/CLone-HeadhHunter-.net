﻿namespace HeadHunterVer1._0.ViewModels;

public class AboutViewModel
{
    public string PathFile { get; set; }
    public string NameCompanyOrUser { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public string userId  { get; set; }
    public List<VacancyViewModel> VacancyViewModels { get; set; }
    public string NewPassword { get; set; }
    public string OldPassword { get; set; }
    public IFormFile AvatarFile { get; set; }
}