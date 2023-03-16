using AutoMapper;
using BlazorPeliculas.Shared.DTOs;
using BlazorPeliculas.Shared.Entidades;

namespace BlazorPeliculas.Server.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Actor, Actor>()
                .ForMember(x => x.Foto2, option => option.Ignore());

            CreateMap<Pelicula, Pelicula>()
                .ForMember(x => x.Poster2, option => option.Ignore());

            CreateMap<VotoPeliculaDTO, VotoPelicula>();
        }
    }
}
