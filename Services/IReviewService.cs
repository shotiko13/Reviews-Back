using ReviewAppProject.Models;

namespace ReviewAppProject.Services;

public interface IReviewService
{
    Task<IEnumerable<Review>> GetAllReviewsAsync();
    Task<Review> GetReviewByIdAsync(int id);
    Task CreateReviewAsync(Review review);
    Task DeleteReviewAsync(int id);
    Task UpdateReviewAsync(Review review);

}