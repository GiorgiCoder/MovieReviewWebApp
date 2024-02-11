using Microsoft.EntityFrameworkCore;
using MovieReviewApp.Data;
using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;

namespace MovieReviewApp.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Review>> GetMovieReviews(int movieId)
        {
            var reviews = await _context.Reviews.Where(r => r.MovieId == movieId).ToListAsync();
            return reviews;
        }

        public async Task<Review?> GetReviewOfUser(int movieId, int userId)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(r => r.MovieId == movieId && r.UserId == userId);
            return review;
        }

        public async Task<bool> AddReview(int movieId, int userId, Review review)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == movieId);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user != null && movie != null)
            {
                review.MovieId = movie.Id;
                review.Movie = movie;
                review.UserId = userId;
                review.User = user;
                
                _context.Add(review);
                return Save();
            }

            return false;
        }

        public bool ReviewExists(int movieId, int userId)
        {
            return _context.Reviews.Any(r => r.MovieId ==  movieId && r.UserId == userId);
        }


        public bool UpdateReview(Review review)
        {
            _context.Update(review);
            return Save();
        }

        public bool DeleteReview(Review review)
        {
            _context.Remove(review);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
