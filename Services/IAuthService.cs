using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using ReviewAppProject.Models;

namespace ReviewAppProject.Services;

public interface IAuthService
{
    Task<IdentityResult> RegisterAsync(string userName, string email, string password);
    Task<string> LoginAsync(string email, string password);
    Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string currentPassword, string newPassword);
    string GenerateJwtToken(ApplicationUser user);
}