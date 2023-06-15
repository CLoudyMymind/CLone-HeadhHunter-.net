namespace HeadHunterVer1._0.ViewModels;

public class ApplicationViewModel
{
    public string Id { get; set; }

    public bool IsAcceptOrRejectedResponse { get; set; }


    public ResumeViewModel ResumeViewModel { get; set; }
    
    public VacancyViewModel VacancyViewModel{ get; set; }
}