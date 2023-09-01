using ReviewAppProject.Models;

namespace ReviewAppProject.Services;

public interface IUserService
{
    Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
    Task<ApplicationUser> GetUserByIdAsync(string userId);
    Task<ApplicationUser> CreateUserAsync(ApplicationUser user);
    Task<ApplicationUser> UpdateUserAsync(ApplicationUser user);
    Task DeleteUserAsync(string userId);
}