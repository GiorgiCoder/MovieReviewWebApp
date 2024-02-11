using MovieReviewApp.Models;

namespace MovieReviewApp.Interfaces
{
    public interface IDirectorRepository
    {
        public Task<IEnumerable<Director>> GetAllDirectors();
        public Task<Director?> GetDirectorById(int id);
        public Task<Director?> GetDirectorByName(string name);
        public Task<IEnumerable<Movie>> GetMoviesOfDirector(int directorId);
        public Task<Country?> GetDirectorCountry(int directorId);
        public bool DirectorExists(string name, DateTime DoB);
        public bool DirectorExists(int directorId);
        public bool CreateDirector(Director director);
        public bool UpdateDirector(Director director);
        public bool Save();
    }
}
