using HeadHunterVer1._0.ViewModels;

namespace HeadHunterVer1._0.Services.Abstractions;

public interface ICategoryService
{
    public Task<List<CategoryViewModel>> GetAllCategoryListAsync();
    public List<CategoryViewModel> GetAllCategoryList();

}