namespace MovieReviewApp.Dto
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Length { get; set; }
        public int ReleaseYear { get; set; }
        public long Budget { get; set; }
        public long BoxOffice { get; set; }
    }
}
