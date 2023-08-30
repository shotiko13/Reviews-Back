using Microsoft.AspNetCore.Identity;

namespace ReviewAppProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public Role UserRole { get; set; }
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
        public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public enum Role
        {
            Admin, User
        }
    }
}
