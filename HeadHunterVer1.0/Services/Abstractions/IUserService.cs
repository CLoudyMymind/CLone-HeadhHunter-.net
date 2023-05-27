using System.Security.Claims;
using HeadHunterVer1._0.Models;

namespace HeadHunterVer1._0.Services.Abstractions;

// Для работы с пользователями, поиска и другой бизнес-логики, связанной с управлением пользователями
public interface IUserService
{
    Task<User?> UserSearchAsync(string? id, ClaimsPrincipal user);

}