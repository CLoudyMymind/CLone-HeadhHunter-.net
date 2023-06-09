using System.Security.Claims;
using HeadHunterVer1._0.Context;
using HeadHunterVer1._0.Extensions;
using HeadHunterVer1._0.Extensions.Enums;
using HeadHunterVer1._0.Models.Employee;
using HeadHunterVer1._0.Models.Employer;
using HeadHunterVer1._0.Services.Abstractions;
using HeadHunterVer1._0.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HeadHunterVer1._0.Services;

public class HomeService : IHomeService
{
    private readonly HeadHunterContext _db;
    private readonly IEmployerService _employerService;
    private readonly MapTo _mapTo;


    public HomeService(HeadHunterContext db, IEmployerService employerService, MapTo mapTo)
    {
        _db = db;
        _employerService = employerService;
        _mapTo = mapTo;
    }

    private IQueryable<Vacancy> GetQueryableVacancy()
    {
        return _db.Vacancies.Include(v => v.Category).OrderByDescending(v => v.UpdateVacancyBid);
    }

    private IQueryable<Resume> GetQueryableResume()
    {
        return _db.Resumes.Include(r => r.Category)
            .Include(r => r.Courses)
            .Include(r => r.WorkExperiences)
            .OrderByDescending(r => r.UpdatedAt);
    }

    public async Task<HomeIndexViewModel> FilterProd(ClaimsPrincipal user, SearchFilterViewModel searchFilter, SortState sortState = SortState.CategoryAsc,
        int currentPage = 1)
    {
        if (user.IsInRole("employee"))
        {
            var vacancyViewModel = GetQueryableVacancy();
            if (searchFilter.Name != null)
            {
                var data = vacancyViewModel
                    .WhereIf(!string.IsNullOrEmpty(searchFilter.Name), u => u.Title.ToLower().Contains(searchFilter.Name.ToLower()));
                if (!data.Any())
                    data = GetQueryableVacancy()
                        .WhereIf(!string.IsNullOrEmpty(searchFilter.Name), u => u.Category.Name.ToLower().Contains(searchFilter.Name.ToLower()));
                vacancyViewModel = data;
            }
            vacancyViewModel = sortState switch
            { 
                SortState.CategoryAsc => vacancyViewModel.OrderBy(x => x.Category.Name),
                SortState.CategodyDesc => vacancyViewModel.OrderByDescending(x => x.Category.Name),
                SortState.PriceAsc => vacancyViewModel.OrderBy(x => x.Wages),
                SortState.PriceDesc => vacancyViewModel.OrderByDescending(x => x.Wages),
                _ => throw new ArgumentOutOfRangeException(nameof(sortState), sortState, null)
            };
            var pageSize = 20;
            var count = vacancyViewModel.Count();
            vacancyViewModel = vacancyViewModel.Skip((currentPage - 1) * pageSize).Take(pageSize);
            var model = new HomeIndexViewModel
            {
                VacancyViewModel = await _mapTo.MapIQueryableVacancyToVacancyViewModel(vacancyViewModel),
                CategorySort = sortState is SortState.CategoryAsc ? SortState.CategodyDesc : SortState.CategoryAsc,
                PriceSort = sortState is SortState.PriceAsc ? SortState.PriceDesc : SortState.PriceAsc,
                PaginationViewModel = new PaginationViewModel(count, currentPage, pageSize),
            };
            return model;
        }

        if (user.IsInRole("employer"))
        {
            var resumeViewModel = GetQueryableResume();
            resumeViewModel = sortState switch
            {
                SortState.CategoryAsc => resumeViewModel.OrderBy(x => x.Category.Name),
                SortState.CategodyDesc => resumeViewModel.OrderByDescending(x => x.Category.Name),
                _ => throw new ArgumentOutOfRangeException(nameof(sortState), sortState, null)
            };

            var pageSize = 20;
            var count = resumeViewModel.Count();
            resumeViewModel = resumeViewModel.Skip((currentPage - 1) * pageSize).Take(pageSize);
            var model = new HomeIndexViewModel
            {
                ResumeViewModels = await _mapTo.MapIQueryableResumeToResumeViewModel(resumeViewModel),
                CategorySort = sortState is SortState.CategoryAsc ? SortState.CategodyDesc : SortState.CategoryAsc,
                PriceSort = sortState is SortState.PriceAsc ? SortState.PriceDesc : SortState.PriceAsc,
                PaginationViewModel = new PaginationViewModel(count, currentPage, pageSize),
            };
            return model;
            
        }

        throw new Exception("Error");
    }
}