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
        var review = await _dbContext.Reviews.FindAsync(id);
        if (review == null)
            throw new ArgumentNullException($"No review found with Id - {id}");
        return review;
    }

    public async Task CreateReviewAsync(Review review)
    {
        await _dbContext.Reviews.AddAsync(review);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteReviewAsync(int id)
    {
        var review = await GetReviewByIdAsync(id);
        _dbContext.Reviews.Remove(review);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateReviewAsync(Review review)
    {
        await GetReviewByIdAsync(review.ReviewId);
        _dbContext.Reviews.Update(review);
        await _dbContext.SaveChangesAsync();
    }

}