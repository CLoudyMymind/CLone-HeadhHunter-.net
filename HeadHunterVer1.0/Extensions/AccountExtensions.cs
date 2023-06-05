using HeadHunterVer1._0.Models;
using HeadHunterVer1._0.Services;
using HeadHunterVer1._0.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace HeadHunterVer1._0.Extensions
{
    public  class AccountExtensions
    {
        private readonly UserManager<User> _userManager;

        public AccountExtensions(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AboutViewModel> EmployeeAboutViewModelExtensions(IdentityUser user, string pathFile, User userData)
        {
            var roles = await _userManager.GetRolesAsync(userData);
            var role = roles.FirstOrDefault(); 

            var aboutViewModel = new AboutViewModel
            {
                PathFile = pathFile,
                Email = user.Email,
                NameCompanyOrUser = user.UserName,
                Role = role,
                PhoneNumber = user.PhoneNumber,

            };
            return aboutViewModel;
        }
        public async Task<AboutViewModel> EmployerAboutViewModelExtensions(IdentityUser user, string pathFile, User userData, List<VacancyViewModel>? vacancyViewModel)
        {
            var roles = await _userManager.GetRolesAsync(userData);
            var role = roles.FirstOrDefault(); 

            var aboutViewModel = new AboutViewModel
            {
                PathFile = pathFile,
                Email = user.Email,
                NameCompanyOrUser = user.UserName,
                Role = role,
                userId = userData.Id,
                PhoneNumber = user.PhoneNumber,
            };
            if (vacancyViewModel != null)
                aboutViewModel.VacancyViewModels = vacancyViewModel;
            return aboutViewModel;
        }




        public User UserModelExtensions(RegisterViewModel model, string imgPath)
        {
            return  new()
            {
                Email = model.Email,
                UserName = model.NameCompanyOrUser,
                PathFile = imgPath!,
                PhoneNumber = model.PhoneNumber
            };
        }
        public async Task<EditAccountProfileViewModels> EditAccountProfileExtensions(User data)
        {
            var userRoles = await _userManager.GetRolesAsync(data);
            return new EditAccountProfileViewModels
            {
                NameCompanyOrUser = data.UserName,
                Email = data.Email,
                Role = userRoles.FirstOrDefault(),
            };
        }
    }
}