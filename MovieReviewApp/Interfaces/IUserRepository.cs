using MovieReviewApp.Models;

namespace MovieReviewApp.Interfaces
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetAllUsers();
        public Task<User?> GetUserById(int id);
        public Task<IEnumerable<Review>> GetReviewsOfUser(int userId);
        public Task<Review?> GetSpecificReviewOfUser(int userId, int reviewId);
        public bool UserExists(string name);
        public bool UserExists(int userId);
        public bool CreateUser(User user);
        public bool UpdateUser(User user);
        public Task<bool> DeleteUser(User user);
        public bool Save();
    }
}
