using AutoMapper;
using FilmesAPI.Controllers;
using FilmesAPI.Data.DTOs.Cinema;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles
{
    public class CinemaProfile : Profile
    {
        public CinemaProfile()
        {
            CreateMap<CreateCinemaDto, Cinema>();
            CreateMap<Cinema, CreateCinemaDto>();
            CreateMap<Cinema, ReadCinemaDto>();
            CreateMap<UpdateCinemaDto, Cinema>();
        }
    }
}