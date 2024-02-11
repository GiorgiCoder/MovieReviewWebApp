using MovieReviewApp.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieReviewApp.Dto
{
    public class ReviewDto
    {
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }
        public string Content { get; set; }
    }
}
