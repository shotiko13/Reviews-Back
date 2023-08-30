using Microsoft.EntityFrameworkCore;
using ReviewAppProject.Data;
using ReviewAppProject.Models;

namespace ReviewAppProject.Services;

public class CommentService : ICommentService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IReviewService _reviewService;

    public CommentService(ApplicationDbContext dbContext, IReviewService reviewService)
    {
        _dbContext = dbContext;
        _reviewService = reviewService;
    }
    
    public async Task<Comment> GetCommentAsync(int commentId)
    {
        return await _dbContext.Comments.FindAsync(commentId);
    }

    public async Task<IEnumerable<Comment>> GetCommentsForReviewAsync(int reviewId)
    {
        await _reviewService.ValidateReviewExists(reviewId);
        return await _dbContext.Comments
                               .Where(comment => comment.ReviewId == reviewId)
                               .ToListAsync();
    }

    public async Task AddCommentAsync(Comment comment)
    {
        await _dbContext.Comments.AddAsync(comment);
        await _dbContext.SaveChangesAsync();
    }

    public async Task EditCommentAsync(Comment comment)
    {
        await ValidateCommentExists(comment.CommentId);
        _dbContext.Comments.Update(comment);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteCommentAsync(int commentId)
    {
        var comment = await ValidateCommentExists(commentId);
        _dbContext.Comments.Remove(comment);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Comment> ValidateCommentExists(int commentId)
    {
        var comment = await GetCommentAsync(commentId);
        if (comment == null)
            throw new ArgumentNullException($"Comment with id - {commentId} couldn't be found");
        return comment;
    }
}