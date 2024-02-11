using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieReviewApp.Dto;
using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;
using MovieReviewApp.Repositories;

namespace MovieReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : Controller
    {
        private readonly IActorRepository _actorRepository;
        private readonly IMapper _mapper;

        public ActorController(IActorRepository actorRepository, IMapper mapper)
        {
            _actorRepository = actorRepository;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ActorDto>))]
        public async Task<IActionResult> GetActors()
        {
            var actors = await _actorRepository.GetAllActors();
            var actorsDto = _mapper.Map<List<ActorDto>>(actors);
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(actorsDto);
        }


        [HttpGet("{actorId}")]
        [ProducesResponseType(200, Type = typeof(ActorDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetActorById(int actorId)
        {
            var actor = await _actorRepository.GetActorById(actorId);
            var actorDto = _mapper.Map<ActorDto>(actor);
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(actorDto);
        }


        [HttpGet("{actorId}/movies")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MovieDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetMoviesOfActor(int actorId)
        {
            var movies = await _actorRepository.GetMoviesOfActor(actorId);
            var movieDtos = _mapper.Map<List<MovieDto>>(movies);
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(movieDtos);
        }


        [HttpGet("{actorId}/country")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCountryOfActor(int actorId)
        {
            var country = await _actorRepository.GetActorCountry(actorId);
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(country);
        }


        [HttpPost("add")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult AddActor([FromQuery] int countryId, [FromBody] ActorDto actorDto)
        {
            if (actorDto == null) { return BadRequest(ModelState); }

            if (_actorRepository.ActorExists(actorDto.Name, actorDto.DoB.Date))
            {
                ModelState.AddModelError("", "Actor already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var actor = _mapper.Map<Actor>(actorDto);
            actor.CountryId = countryId;
            var created = _actorRepository.CreateActor(actor);

            if (!created)
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully added!");
        }


        [HttpPut("{actorId}/edit")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateActor(int actorId, int countryId, [FromBody] ActorDto actorDto)
        {
            if(actorDto == null || actorId != actorDto.Id)
            {
                return BadRequest(ModelState);
            }
            
            if (!_actorRepository.ActorExists(actorDto.Id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var actor = _mapper.Map<Actor>(actorDto);
            actor.CountryId = countryId;
            var updated = _actorRepository.UpdateActor(actor);

            if (!updated)
            {
                ModelState.AddModelError("", "Something went wrong while updating the actor");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{actorId}/delete")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteMovie(int actorId)
        {
            if (!_actorRepository.ActorExists(actorId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var actor = await _actorRepository.GetActorById(actorId);
            var deleted = await _actorRepository.DeleteActor(actor!);

            if (!deleted)
            {
                ModelState.AddModelError("", "Something went wrong while deleting the movie");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
