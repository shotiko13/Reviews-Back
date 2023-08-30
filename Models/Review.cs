using System.ComponentModel.DataAnnotations;

namespace ReviewAppProject.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public string ReviewName { get; set; }
        public string ArtName { get; set; }
        public ArtGroup ArtGroup { get; set; }
        public string ReviewText { get; set; }
        public string ImageUrl { get; set; }
        [Range(1,10)]
        public int Grade { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        public string? UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }

        public virtual ICollection<TagReview> TagReviews { get; set; } = new List<TagReview>();
        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }

    public enum ArtGroup
    {
        Movies,
        Books,
        Games,
    }

}
