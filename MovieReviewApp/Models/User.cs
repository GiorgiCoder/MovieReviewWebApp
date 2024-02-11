using System.ComponentModel.DataAnnotations.Schema;

namespace MovieReviewApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImagePath { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
    }
}
