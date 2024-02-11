using Microsoft.EntityFrameworkCore;
using MovieReviewApp.Data;
using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;

namespace MovieReviewApp.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext _context;

        public MovieRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            var movies = await _context.Movies.ToListAsync();
            return movies;
        }

        public async Task<Movie?> GetMovieById(int id)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
            return movie;
        }

        public async Task<Movie?> GetMovieWithTitle(string title)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Title == title);
            return movie;
        }

        public async Task<IEnumerable<Movie>> GetMoviesOfDirector(int directorId)
        {
            var movies = await _context.Movies.Where(m => m.DirectorId == directorId).ToListAsync();
            return movies;
        }

        
        public async Task<double> GetMovieRating(int movieId)
        {
            var ratings = await _context.Reviews.Where(r => r.MovieId == movieId).Select(r => r.Rating).ToListAsync();
            if (!ratings.Any()) { return 0; }
            var averageRating = ((double)ratings.Sum()) / ratings.Count;
            return averageRating;
        }

        public bool MovieExists(int movieId)
        {
            return _context.Movies.Any(m => m.Id == movieId);
        }

        public bool MovieExists(string title, int releaseYear)
        {
            return _context.Movies.Any(m => m.Title == title && m.ReleaseYear == releaseYear);
        }

        public bool AddMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            return Save();
        }

        public bool UpdateMovie(Movie movie)
        {
            _context.Movies.Update(movie);
            return Save();
        }

        public async Task<bool> DeleteMovie(Movie movie)
        {
            var casts = await _context.ActorMovies.Where(x => x.MovieId == movie.Id).ToListAsync();
            foreach (var cast in casts)
            {
                _context.Remove(cast);
            }
            var movieGenres = await _context.MovieGenres.Where(x => x.MovieId == movie.Id).ToListAsync();
            foreach (var movieGenre in movieGenres)
            {
                _context.Remove(movieGenre);
            }
            var movieReviews = await _context.Reviews.Where(x => x.MovieId == movie.Id).ToListAsync(); // this should work without it but still
            foreach (var movieReview in movieReviews)
            {
                _context.Remove(movieReview);
            }
            _context.Remove(movie);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
