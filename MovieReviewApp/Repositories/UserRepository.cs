using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieReviewApp.Data;
using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;

namespace MovieReviewApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<User?> GetUserById(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

        public async Task<IEnumerable<Review>> GetReviewsOfUser(int userId)
        {
            var reviews = await _context.Reviews.Where(r => r.UserId == userId).ToListAsync();
            return reviews;
        }

        public async Task<Review?> GetSpecificReviewOfUser(int userId, int movieId)
        {
            var review = await _context.Reviews.Where(r => r.UserId == userId).FirstOrDefaultAsync(r => r.MovieId == movieId);
            return review;
        }

        public bool UserExists(string name)
        {
            return _context.Users.Any(x => x.Name == name);
        }

        public bool UserExists(int userId)
        {
            return _context.Users.Any(x => x.Id == userId);
        }

        public bool CreateUser(User user)
        {
            _context.Add(user);
            return Save();
        }

        public bool UpdateUser(User user)
        {
            _context.Update(user);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
