using HeadHunterVer1._0.Context;
using HeadHunterVer1._0.Models.Employee;
using HeadHunterVer1._0.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace HeadHunterVer1._0.Services;

public class ResumeService : IResumeService
{
    private readonly HeadHunterContext _db;

    public ResumeService(HeadHunterContext db)
    {
        _db = db;
    }

    public async Task<List<Resume>?> GetAllResumesAsync()
    {
        return await _db.Resumes
            .Include(r => r.Employee)
            .Include(r => r.Category)
            .Include(r => r.Courses)
            .Include(r => r.WorkExperiences)
            .ToListAsync();
    }

    public async Task<Resume?> GetResumeById(int id)
    {
       return await _db.Resumes
            .Include(r => r.Employee)
            .Include(r => r.Category)
            .Include(r => r.Courses)
            .Include(r => r.WorkExperiences)
            .FirstOrDefaultAsync(r => r.Id == id);
    }
}