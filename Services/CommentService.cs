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
        var comment = await _dbContext.Comments.FindAsync(commentId);
        if (comment == null)
            throw new ArgumentNullException($"No comment with id - {commentId} found");
        return comment;
    }

    public async Task<IEnumerable<Comment>> GetCommentsForReviewAsync(int reviewId)
    {
        await _reviewService.GetReviewByIdAsync(reviewId);
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
        await GetCommentAsync(comment.CommentId);
        _dbContext.Comments.Update(comment);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteCommentAsync(int commentId)
    {
        var comment = await GetCommentAsync(commentId);
        _dbContext.Comments.Remove(comment);
        await _dbContext.SaveChangesAsync();
    }

}