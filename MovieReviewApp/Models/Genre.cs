namespace MovieReviewApp.Models
{
    public class Genre // look-up table
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<MovieGenre> Movies { get; set; }
    }
}
