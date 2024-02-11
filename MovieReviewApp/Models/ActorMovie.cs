using System.ComponentModel.DataAnnotations.Schema;

namespace MovieReviewApp.Models
{
    public class ActorMovie
    {
        public Actor Actor { get; set; }
        [ForeignKey("Actor")]
        public int ActorId { get; set; }
        public Movie Movie { get; set; }
        [ForeignKey("Movie")]
        public int MovieId { get; set; }
        public int? ScreenTime { get; set; }
        public int Salary { get; set; }
        
    }
}
