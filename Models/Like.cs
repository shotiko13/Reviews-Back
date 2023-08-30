namespace ReviewAppProject.Models
{
    public class Like
    {
        public int LikeId { get; set; }

        public string? UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int ReviewId { get; set; }
        public virtual Review Review { get; set; }
    }
}
