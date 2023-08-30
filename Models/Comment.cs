namespace ReviewAppProject.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }

        public string? UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int ReviewId { get; set; }
        public virtual Review Review { get; set; }
    }
}
