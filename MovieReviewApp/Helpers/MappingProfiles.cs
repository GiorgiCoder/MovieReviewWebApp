using AutoMapper;
using MovieReviewApp.Dto;
using MovieReviewApp.Models;

namespace MovieReviewApp.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Movie, MovieDto>().ReverseMap();
            CreateMap<Actor, ActorDto>().ReverseMap();
            CreateMap<Director, DirectorDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Review, ReviewDto>().ReverseMap();
        }
    }
}
