using System.ComponentModel.DataAnnotations.Schema;

namespace MovieReviewApp.Models
{
    public class Review
    {
        public User User { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public Movie Movie { get; set; }
        [ForeignKey("Movie")]
        public int MovieId { get; set;}
        public int Rating { get; set; }
        public string Content { get; set; }
        public int Upvotes { get; set; }
        public int Downvotes { get; set; }
    }
}
