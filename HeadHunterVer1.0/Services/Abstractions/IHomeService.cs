using System.Security.Claims;
using HeadHunterVer1._0.Extensions.Enums;
using HeadHunterVer1._0.ViewModels;

namespace HeadHunterVer1._0.Services.Abstractions;

public interface IHomeService
{
    Task<HomeIndexViewModel> FilterProd(ClaimsPrincipal user, SearchFilterViewModel searchFilter,
        SortState sortState = SortState.CategoryAsc,
        int currentPage = 1);
}