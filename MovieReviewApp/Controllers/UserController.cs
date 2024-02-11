using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieReviewApp.Dto;
using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;

namespace MovieReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userRepository.GetAllUsers();
            var usersDto = _mapper.Map<List<UserDto>>(users);
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(usersDto);
        }


        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetUser(int userId)
        {
            var user = await _userRepository.GetUserById(userId);
            var userDto = _mapper.Map<UserDto>(user);
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(userDto);
        }


        [HttpGet("{userId}/reviews")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetReviewsOfUser(int userId)
        {
            var reviews = await _userRepository.GetReviewsOfUser(userId);
            var reviewsDto = _mapper.Map<List<ReviewDto>>(reviews);
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(reviewsDto);
        }


        [HttpGet("{userId}/reviews/{movieId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetUserReviewOnMovie(int userId, int movieId)
        {
            var review = await _userRepository.GetSpecificReviewOfUser(userId, movieId);
            var reviewDto = _mapper.Map<ReviewDto>(review);
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(reviewDto);
        }

        [HttpPost("add")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult AddUser([FromBody] UserDto userDto)
        {
            if(userDto == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_userRepository.UserExists(userDto.Id))
            {
                ModelState.AddModelError("", "User already exists");
                return StatusCode(422, ModelState);
            }

            var user = _mapper.Map<User>(userDto);
            var added = _userRepository.CreateUser(user);

            if (!added)
            {
                ModelState.AddModelError("", "Something went wrong while adding");
                return StatusCode(500, ModelState);
            }

            return Ok(user);
        }

        [HttpPut("{userId}/edit")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser(int userId, [FromBody] UserDto userDto)
        {
            if (userDto == null || userId != userDto.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_userRepository.UserExists(userId))
            {
                return NotFound();
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = _mapper.Map<User>(userDto);
            var updated = _userRepository.UpdateUser(user!);

            if (!updated)
            {
                ModelState.AddModelError("", "Something went wrong while updating the user");
            }

            return NoContent();
        }

    }
}
