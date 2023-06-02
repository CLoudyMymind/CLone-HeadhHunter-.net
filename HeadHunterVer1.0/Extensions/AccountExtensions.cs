﻿using HeadHunterVer1._0.Models;
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

        public async Task<AboutViewModel> CreateAboutViewModelExtensions(IdentityUser user, string pathFile, User userData)
        {
            var roles = await _userManager.GetRolesAsync(userData);
            var role = roles.FirstOrDefault(); 
            return new AboutViewModel
            {
                PathFile = pathFile,
                Email = user.Email,
                NameCompanyOrUser = user.UserName,
                Role = role
            };
        }


        public User UserModelExtensions(RegisterViewModel model, string imgPath)
        {
            return  new()
            {
                Email = model.Email,
                UserName = model.NameCompanyOrUser,
                PathFile = imgPath!
            };
        }
        public async Task<EditAccountProfileViewModels> EditAccountProfileExtensions(User data)
        {
            var userRoles = await _userManager.GetRolesAsync(data);
            return new EditAccountProfileViewModels
            {
                NameCompanyOrUser = data.UserName,
                Email = data.Email,
                Role = userRoles.FirstOrDefault()
            };
        }
    }
}