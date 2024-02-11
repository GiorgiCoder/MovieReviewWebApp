using Microsoft.EntityFrameworkCore;
using MovieReviewApp.Data;
using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;

namespace MovieReviewApp.Repositories
{
    public class DirectorRepository : IDirectorRepository
    {
        private readonly ApplicationDbContext _context;

        public DirectorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Director>> GetAllDirectors()
        {
            var directors = await _context.Directors.ToListAsync();
            return directors;
        }

        public async Task<Director?> GetDirectorById(int id)
        {
            var director = await _context.Directors.FirstOrDefaultAsync(d => d.Id == id);
            return director;
        }

        public async Task<Director?> GetDirectorByName(string name)
        {
            var director = await _context.Directors.FirstOrDefaultAsync(d => d.Name == name);
            return director;
        }

        public async Task<IEnumerable<Movie>> GetMoviesOfDirector(int directorId)
        {
            var movies = await _context.Movies.Where(m => m.DirectorId == directorId).ToListAsync();
            return movies;
        }

        public async Task<Country?> GetDirectorCountry(int directorId)
        {
            var director = await GetDirectorById(directorId);
            if (director == null) { return null; }
            var country = await _context.Countries.FirstOrDefaultAsync(c => c.Id == director.CountryId);
            return country;
        }

        public bool DirectorExists(string name, DateTime DoB)
        {
            return _context.Actors.Any(a => a.Name == name && a.DoB == DoB.Date);
        }

        public bool DirectorExists(int directorId)
        {
            return _context.Directors.Any(d => d.Id == directorId);
        }

        public bool CreateDirector(Director director)
        {
            _context.Add(director);
            return Save();
        }

        public bool UpdateDirector(Director director)
        {
            _context.Update(director);
            return Save();
        }

        public async Task<bool> DeleteDirector(Director director)
        {
            var directorMovies = await _context.Movies.Where(m => m.DirectorId == director.Id).ToListAsync();
            foreach(var movie in directorMovies)
            {
                movie.DirectorId = 10;
                movie.Director = await GetDirectorById(10)!;
            }
            _context.Remove(director);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
