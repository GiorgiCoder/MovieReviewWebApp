using System.ComponentModel.DataAnnotations.Schema;

namespace MovieReviewApp.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<MovieGenre> Genres { get; set; }
        public int Length { get; set; }
        public int ReleaseYear { get; set; }
        public long Budget { get; set; }
        public long BoxOffice { get; set; }
        public IEnumerable<ActorMovie> Cast { get; set; }
        public Director Director { get; set; }
        [ForeignKey("Director")]
        public int DirectorId { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
    }
}
