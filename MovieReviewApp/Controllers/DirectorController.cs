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
    public class DirectorController : Controller
    {
        private readonly IDirectorRepository _directorRepository;
        private readonly IMapper _mapper;

        public DirectorController(IDirectorRepository directorRepository, IMapper mapper)
        {
            _directorRepository = directorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DirectorDto>))]
        public async Task<IActionResult> GetDirectors()
        {
            var directors = await _directorRepository.GetAllDirectors();
            var directorsDto = _mapper.Map<List<DirectorDto>>(directors);
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(directorsDto);
        }

        [HttpGet("{directorId}")]
        [ProducesResponseType(200, Type = typeof(DirectorDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetDirectorById(int directorId)
        {
            var director = await _directorRepository.GetDirectorById(directorId);
            var directorDto = _mapper.Map<DirectorDto>(director);
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(directorDto);
        }

        [HttpGet("{directorId}/movies")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MovieDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetMoviesOfDirector(int directorId)
        {
            var movies = await _directorRepository.GetMoviesOfDirector(directorId);
            var movieDtos = _mapper.Map<List<MovieDto>>(movies);
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(movieDtos);
        }

        [HttpGet("{directorId}/country")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCountryOfActor(int directorId)
        {
            var country = await _directorRepository.GetDirectorCountry(directorId);
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(country);
        }

        [HttpPost("add")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult AddDirector([FromQuery] int countryId, [FromBody] DirectorDto directorDto)
        {
            if (directorDto == null) { return BadRequest(ModelState); }

            if (_directorRepository.DirectorExists(directorDto.Name, directorDto.DoB.Date))
            {
                ModelState.AddModelError("", "Actor already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var director = _mapper.Map<Director>(directorDto);
            director.CountryId = countryId;
            var created = _directorRepository.CreateDirector(director);

            if (!created)
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully added!");
        }

        [HttpPut("{directorId}/edit")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateActor(int directorId, int countryId, [FromBody] DirectorDto directorDto)
        {
            if (directorDto == null || directorId != directorDto.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_directorRepository.DirectorExists(directorDto.Id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var director = _mapper.Map<Director>(directorDto);
            director.CountryId = countryId;
            var updated = _directorRepository.UpdateDirector(director);

            if (!updated)
            {
                ModelState.AddModelError("", "Something went wrong while updating the director");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{directorId}/delete")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteMovie(int directorId) // when deleting director, movies of this director
                                                                     // go to dummy director (with Id = 10) (see in repository)
        {
            if (!_directorRepository.DirectorExists(directorId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var director = await _directorRepository.GetDirectorById(directorId);
            var deleted = await _directorRepository.DeleteDirector(director!);

            if (!deleted)
            {
                ModelState.AddModelError("", "Something went wrong while deleting the movie");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
