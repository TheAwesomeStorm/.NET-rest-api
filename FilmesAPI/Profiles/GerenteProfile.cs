using System.Linq;
using AutoMapper;
using FilmesAPI.Data.DTOs.Gerente;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles
{
    public class GerenteProfile : Profile
    {
        public GerenteProfile()
        {
            CreateMap<CreateGerenteDto, Gerente>();
            CreateMap<Gerente, ReadGerenteDto>()
                .ForMember(gerente => gerente.Cinemas, options => options
                    .MapFrom(gerente => gerente.Cinemas
                        .Select(cinema => new {
                            cinema.Id,
                            cinema.Nome,
                            cinema.Endereco,
                            cinema.EnderecoId
                        })));
        }
    }
}