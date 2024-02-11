using Microsoft.EntityFrameworkCore;
using MovieReviewApp.Models;

namespace MovieReviewApp.Interfaces
{
    public interface IActorRepository
    {
        public Task<IEnumerable<Actor>> GetAllActors();
        public Task<Actor?> GetActorById(int id);
        public Task<Actor?> GetActorByName(string name);
        public Task<IEnumerable<Movie>> GetMoviesOfActor(int actorId);
        public Task<Country?> GetActorCountry(int actorId);
        public bool ActorExists(string name, DateTime DoB);
        public bool ActorExists(int actorId);
        public bool CreateActor(Actor actor);
        public bool UpdateActor(Actor actor);
        public Task<bool> DeleteActor(Actor actor);
        public bool Save();
    }
}
