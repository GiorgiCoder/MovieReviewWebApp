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
    public class MovieController : Controller
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        private readonly IReviewRepository _reviewRepository;

        public MovieController(IMovieRepository movieRepository, IMapper mapper, IReviewRepository reviewRepository)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
            _reviewRepository = reviewRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MovieDto>))]      
        public async Task<IActionResult> GetMovies()
        {
            var movies = await _movieRepository.GetAllMovies();
            var moviesDto = _mapper.Map<List<MovieDto>>(movies);
            if(!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(moviesDto);
        }

        [HttpGet("{movieId}")]
        [ProducesResponseType(200, Type = typeof(MovieDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetMovieById(int movieId)
        {
            var movie = await _movieRepository.GetMovieById(movieId);
            var movieDto = _mapper.Map<MovieDto>(movie);
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(movieDto);
        }

        [HttpGet("{MovieId}/rating")]
        [ProducesResponseType(200, Type = typeof(double))]
        public async Task<IActionResult> GetRatingOfMovie(int MovieId)
        {
            var rating = await _movieRepository.GetMovieRating(MovieId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(rating);
        }

        [HttpGet("{movieId}/reviews")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
        public async Task<IActionResult> GetReviews(int movieId)
        {
            var reviews = await _movieRepository.GetMovieReviews(movieId);
            var reviewsDto = _mapper.Map<List<ReviewDto>>(reviews);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviewsDto);
        }

        [HttpGet("{movieId}/reviews/{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
        public async Task<IActionResult> GetReviewOfUser(int movieId, int userId)
        {
            var review = await _movieRepository.GetReviewOfUser(movieId, userId);
            var reviewDto = _mapper.Map<ReviewDto>(review);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(reviewDto);
        }

        [HttpPost("add")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult AddMovie([FromQuery] int directorId, [FromBody] MovieDto movieDto)
        {
            if (movieDto == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_movieRepository.MovieExists(movieDto.Title, movieDto.ReleaseYear))
            {
                ModelState.AddModelError("", "Movie already exists");
                return StatusCode(422, ModelState);
            }

            var movie = _mapper.Map<Movie>(movieDto);
            movie.DirectorId = directorId;
            var created = _movieRepository.AddMovie(movie);

            if (!created)
            {
                ModelState.AddModelError("", "Error while adding the movie");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created!");
        }


        [HttpPut("{movieId}/edit")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateMovie(int movieId, int directorId, [FromBody] MovieDto movieDto)
        {
            if (movieDto == null || movieDto.Id != movieId)
            {
                return BadRequest(ModelState);
            }

            if (!_movieRepository.MovieExists(movieDto.Id))
            {
                return NotFound();
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movie = _mapper.Map<Movie>(movieDto);
            movie.DirectorId = directorId;
            var updated = _movieRepository.UpdateMovie(movie);

            if (!updated)
            {
                ModelState.AddModelError("", "Something wen wrong while updating movie");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [HttpPost("{movieId}/reviews/add")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddReview(int movieId, [FromQuery] int userId, [FromBody] ReviewDto reviewDto)
        {
            if (reviewDto == null) { return BadRequest(ModelState); }

            var review = _mapper.Map<Review>(reviewDto);

            if (_reviewRepository.ReviewExists(movieId, userId))
            {
                ModelState.AddModelError("", "Review already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var created = await _reviewRepository.AddReview(movieId, userId, review);
            if (!created)
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully added!");
        }


        [HttpPut("{movieId}/reviews/{userId}/edit")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult EditReview(int movieId, int userId, [FromBody] ReviewDto reviewDto)
        {
            if (reviewDto == null || movieId != reviewDto.MovieId || userId != reviewDto.UserId)
            {
                return BadRequest();
            }

            if (!_reviewRepository.ReviewExists(movieId, userId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var review = _mapper.Map<Review>(reviewDto);
            var updated = _reviewRepository.UpdateReview(review);

            if(!updated)
            {
                ModelState.AddModelError("", "Something went wrong while updating review");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{movieId}/delete")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteMovie(int movieId)
        {
            if (!_movieRepository.MovieExists(movieId))
            {
                return NotFound();
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movie = await _movieRepository.GetMovieById(movieId);
            var deleted = _movieRepository.DeleteMovie(movie!);

            if (!deleted)
            {
                ModelState.AddModelError("", "Something went wrong while deleting the movie");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }
    }
}
