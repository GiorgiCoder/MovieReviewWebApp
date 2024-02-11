using System.ComponentModel.DataAnnotations.Schema;

namespace MovieReviewApp.Models
{
    public class MovieGenre
    {
        public Movie Movie { get; set; }
        [ForeignKey("Movie")]
        public int MovieId { get; set; }
        public Genre Genre { get; set; }
        [ForeignKey("Genre")]
        public int GenreId { get; set;}
    }
}
