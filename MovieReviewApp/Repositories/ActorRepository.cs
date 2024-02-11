using Microsoft.EntityFrameworkCore;
using MovieReviewApp.Data;
using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;

namespace MovieReviewApp.Repositories
{
    public class ActorRepository : IActorRepository
    {
        private readonly ApplicationDbContext _context;

        public ActorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Actor>> GetAllActors()
        {
            var actors = await _context.Actors.ToListAsync();
            return actors;
        }

        public async Task<Actor?> GetActorById(int id)
        {
            var actor = await _context.Actors.FirstOrDefaultAsync(a => a.Id == id);
            return actor;
        }

        public async Task<Actor?> GetActorByName(string name)
        {
            var actor = await _context.Actors.FirstOrDefaultAsync(a => a.Name == name);
            return actor;
        }

        public async Task<IEnumerable<Movie>> GetMoviesOfActor(int actorId)
        {
            var movies = await _context.ActorMovies.Where(am => am.ActorId == actorId).Select(am => am.Movie).ToListAsync();
            return movies;
        }

        public async Task<Country?> GetActorCountry(int actorId)
        {
            var actor = await GetActorById(actorId);
            if (actor == null) { return null; }
            var country = await _context.Countries.FirstOrDefaultAsync(c => c.Id == actor.CountryId);
            return country;
        }

        public bool ActorExists(int actorId)
        {
            return _context.Actors.Any(a => a.Id == actorId);
        }

        public bool ActorExists(string name, DateTime DoB)
        {
            return _context.Actors.Any(a => a.Name == name && a.DoB == DoB.Date);
        }

        public bool CreateActor(Actor actor)
        {
            _context.Add(actor);
            return Save();
        }

        public bool UpdateActor(Actor actor)
        {
            _context.Update(actor);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
