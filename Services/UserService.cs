using Microsoft.EntityFrameworkCore;
using ReviewAppProject.Data;
using ReviewAppProject.Models;

namespace ReviewAppProject.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _dbContext;

    public UserService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
    {
        return await _dbContext.Users.ToListAsync();
    }

    public async Task<ApplicationUser> GetUserByIdAsync(string userId)
    {
        var user = await _dbContext.Users.FindAsync(userId);
        if (user == null)
            throw new ArgumentNullException($"User with Id - {userId} not found");
        return user;
    }

    public async Task<ApplicationUser> CreateUserAsync(ApplicationUser user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<ApplicationUser> UpdateUserAsync(ApplicationUser user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }

    public async Task DeleteUserAsync(string userId)
    {
        var user = await GetUserByIdAsync(userId);
        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();
    }
}