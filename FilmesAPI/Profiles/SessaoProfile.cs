using AutoMapper;
using FilmesAPI.Data.DTOs.Gerente;
using FilmesAPI.Data.DTOs.Sessao;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles
{
    public class SessaoProfile : Profile
    {
        public SessaoProfile()
        {
            CreateMap<CreateSessaoDto, Sessao>();
            CreateMap<Sessao, ReadSessaoDto>()
                .ForMember(dto => dto.HorarioDeInicio, options => options
                    .MapFrom(dto => dto.HorarioDeEncerramento.AddMinutes(-1 * dto.Filme.Duracao)));
        }
    }
}