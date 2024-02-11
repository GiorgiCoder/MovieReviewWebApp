using MovieReviewApp.Models;

namespace MovieReviewApp.Interfaces
{
    public interface IReviewRepository
    {
        public Task<IEnumerable<Review>> GetMovieReviews(int movieId);
        public Task<Review?> GetReviewOfUser(int movieId, int userId);
        public bool ReviewExists(int movieId, int userId);
        public Task<bool> AddReview(int movieId, int userId, Review review);
        public bool UpdateReview(Review review);
        public bool DeleteReview(Review review);
        public bool Save();
    }
}
