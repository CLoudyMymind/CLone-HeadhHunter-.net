using HeadHunterVer1._0.Context;
using HeadHunterVer1._0.Extensions;
using HeadHunterVer1._0.Services.Abstractions;
using HeadHunterVer1._0.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HeadHunterVer1._0.Services;

public class CategoryService : ICategoryService
{
    private readonly HeadHunterContext _db;
    private readonly MapTo _mapTo;

    public CategoryService(HeadHunterContext db, MapTo mapTo)
    {
        _db = db;
        _mapTo = mapTo;
    }
    public async Task<List<CategoryViewModel>> GetAllCategoryListAsync() =>
        _mapTo.MapToListCategories(await _db.Categories.ToListAsync());
}