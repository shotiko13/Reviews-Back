using Microsoft.EntityFrameworkCore;
using ReviewAppProject.Data;
using ReviewAppProject.Models;

namespace ReviewAppProject.Services;

public class ReviewService : IReviewService
{
    private readonly ApplicationDbContext _dbContext;

    public ReviewService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<Review>> GetAllReviewsAsync()
    {
        return await _dbContext.Reviews.ToListAsync();
    }

    public async Task<Review> GetReviewByIdAsync(int id)
    {
        return await _dbContext.Reviews.FindAsync(id);
    }

    public async Task CreateReviewAsync(Review review)
    {
        _dbContext.Reviews.AddAsync(review);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteReviewAsync(int id)
    {
        var review = await ValidateReviewExists(id);
        _dbContext.Reviews.Remove(review);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateReviewAsync(Review review)
    {
        await ValidateReviewExists(review.ReviewId);
        _dbContext.Reviews.Update(review);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Review> ValidateReviewExists(int id)
    {
        var review = await GetReviewByIdAsync(id);
        if (review == null)
            throw new ArgumentNullException($"Review with id - {id} couldn't be found");
        return review;
    }
}