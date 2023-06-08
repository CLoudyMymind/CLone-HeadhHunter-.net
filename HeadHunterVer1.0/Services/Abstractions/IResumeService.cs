using HeadHunterVer1._0.Models.Employee;

namespace HeadHunterVer1._0.Services.Abstractions;

public interface IResumeService
{
    Task<List<Resume>?> GetAllResumesAsync();
    Task<Resume?> GetResumeById(int id);
}