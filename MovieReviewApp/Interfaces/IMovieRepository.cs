using MovieReviewApp.Models;

namespace MovieReviewApp.Interfaces
{
    public interface IMovieRepository
    {
        public Task<IEnumerable<Movie>> GetAllMovies();
        public Task<Movie?> GetMovieById(int id);
        public Task<Movie?> GetMovieWithTitle(string title);
        public Task<IEnumerable<Movie>> GetMoviesOfDirector(int directorId);
        public Task<IEnumerable<Review>> GetMovieReviews(int movieId);
        public Task<Review?> GetReviewOfUser(int movieId, int userId);
        public Task<double> GetMovieRating(int movieId);
        public bool MovieExists(string title, int releaseYear);
        public bool MovieExists(int movieId);
        public bool AddMovie(Movie movie);
        public bool UpdateMovie(Movie movie);
        public bool DeleteMovie(Movie movie);
        public bool Save();
    }
}
