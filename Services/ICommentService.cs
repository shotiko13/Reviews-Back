using ReviewAppProject.Models;

namespace ReviewAppProject.Services;

public interface ICommentService
{
    Task<Comment> GetCommentAsync(int commentId);
    Task<IEnumerable<Comment>> GetCommentsForReviewAsync(int reviewId);
    Task AddCommentAsync(Comment comment);
    Task EditCommentAsync(Comment comment);
    Task DeleteCommentAsync(int commentId);
}