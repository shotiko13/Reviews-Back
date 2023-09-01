using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ReviewAppProject.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace ReviewAppProject.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<IdentityResult> RegisterAsync(string userName, string email, string password)
        {
            var user = new ApplicationUser
            {
                Email = email,
                UserName = userName
            };
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<string> LoginAsync(string emailOrUsername, string password)
        {
            var user = await FindByEmailOrUserNameAsync(emailOrUsername);
            if (user == null) throw new Exception("User not found");
            var result = await _signInManager.PasswordSignInAsync(user.UserName, password, false, false);
            if (!result.Succeeded) throw new Exception("Invalid Login Request");
            return GenerateJwtToken(user);
        }

        public async Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string currentPassword, string newPassword)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }

        private async Task<ApplicationUser> FindByEmailOrUserNameAsync(string emailOrUsername)
        {
            var userByEmail = await _userManager.FindByEmailAsync(emailOrUsername);
            if (userByEmail != null) return userByEmail;
            return await _userManager.FindByNameAsync(emailOrUsername);
        }

        public string GenerateJwtToken(ApplicationUser user)
        {
            var tokenDescriptor = BuildTokenDescriptor(user);
            return CreateToken(tokenDescriptor);
        }

        private SecurityTokenDescriptor BuildTokenDescriptor(ApplicationUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            var key = Encoding.UTF8.GetBytes(_configuration["JwtKey"]);
            return new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
        }

        private string CreateToken(SecurityTokenDescriptor tokenDescriptor)
        {
            if (tokenDescriptor == null) throw new ArgumentNullException(nameof(tokenDescriptor));
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
