namespace ReviewAppProject.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public string TagName { get; set; }

        public virtual ICollection<TagReview> TagReviews { get; set; } = new List<TagReview>();
    }
}
