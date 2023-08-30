namespace ReviewAppProject.Models
{
    public class TagReview
    {
        public int TagId { get; set; }
        public Tag Tag { get; set; }

        public int ReviewId { get; set; }
        public Review Review { get; set; }
    }
}
