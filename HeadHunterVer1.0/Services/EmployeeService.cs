using System.Security.Claims;
using HeadHunterVer1._0.Context;
using HeadHunterVer1._0.Extensions;
using HeadHunterVer1._0.Models;
using HeadHunterVer1._0.Models.Employee;
using HeadHunterVer1._0.Services.Abstractions;
using HeadHunterVer1._0.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HeadHunterVer1._0.Services;

public class EmployeeService : IEmployeeService
{
    private readonly HeadHunterContext _db;
    private readonly UserManager<User> _userManager;
    private readonly MapTo _mapTo;

    public EmployeeService(HeadHunterContext db, UserManager<User> userManager, MapTo mapTo)
    {
        _db = db;
        _userManager = userManager;
        _mapTo = mapTo;
    }

    public async Task<List<ResumeViewModel>> GetAllResume()
    {
        return _mapTo.MapResumeToResumeViewModels(
            await _db.Resumes
                .Include(r => r.Courses)
                .Include(r => r.Employee)
                .Include(r => r.Category)
                .Include(r => r.WorkExperiences).ToListAsync());
    }

    public async Task CreateResumeAsync(CreateResumeViewModel viewModel, ClaimsPrincipal user)
    {
        var currentUser = await _userManager.GetUserAsync(user);
        if (currentUser is null)
            throw new Exception("Пользователь не найден");
        
        var newResume = new Resume
        {
            EmployeeId = currentUser.Id,
            NameOfResume = viewModel.NameOfResume,
            ExpectedSalary = viewModel.ExpectedSalary,
            TelegramLink = viewModel.TelegramLink,
            Email = viewModel.Email,
            Phone = viewModel.Phone,
            FacebookLink = viewModel.FacebookLink,
            LinkedInLink = viewModel.LinkedInLink,
            CategoryId = viewModel.CategoryId,
            WorkExperiences = viewModel.WorkExperiences,
            Courses = viewModel.Courses
        };
        await _db.Resumes.AddAsync(newResume);
        await _db.SaveChangesAsync();
    }

  
}