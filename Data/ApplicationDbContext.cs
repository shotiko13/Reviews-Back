using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReviewAppProject.Models;

namespace ReviewAppProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Review> Reviews { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User to Review
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Reviews)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            // Review to Comment
            modelBuilder.Entity<Review>()
                .HasMany(r => r.Comments)
                .WithOne(c => c.Review)
                .HasForeignKey(c => c.ReviewId)
                .OnDelete(DeleteBehavior.Cascade);

            // User to Comment
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            // Review to Like
            modelBuilder.Entity<Review>()
                .HasMany(r => r.Likes)
                .WithOne(l => l.Review)
                .HasForeignKey(l => l.ReviewId)
                .OnDelete(DeleteBehavior.Cascade);

            // User to Like
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Likes)
                .WithOne(l => l.User)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            // Review to Rating
            modelBuilder.Entity<Review>()
                .HasMany(r => r.Ratings)
                .WithOne(ra => ra.Review)
                .HasForeignKey(ra => ra.ReviewId)
                .OnDelete(DeleteBehavior.Cascade);

            // User to Rating
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Ratings)
                .WithOne(ra => ra.User)
                .HasForeignKey(ra => ra.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            // Many-to-Many relationship between Review and Tag using TagReview
            modelBuilder.Entity<TagReview>()
                .HasKey(tr => new { tr.ReviewId, tr.TagId });

            modelBuilder.Entity<TagReview>()
                .HasOne(tr => tr.Review)
                .WithMany(r => r.TagReviews)
                .HasForeignKey(tr => tr.ReviewId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TagReview>()
                .HasOne(tr => tr.Tag)
                .WithMany(t => t.TagReviews)
                .HasForeignKey(tr => tr.TagId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}