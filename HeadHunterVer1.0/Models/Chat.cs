using HeadHunterVer1._0.Models.Employee;
using HeadHunterVer1._0.Models.Employer;

namespace HeadHunterVer1._0.Models;

public class Chat
{
    public string Id { get; set; }

    public DateTime DateSendMessage   { get; set; }

    public User UserEmployee { get; set; }

    public string UserEmployeeId { get; set; }

    public User UserEmployer { get; set; }

    public string UserEmployerId { get; set; }

    public string Message { get; set; }
    

}