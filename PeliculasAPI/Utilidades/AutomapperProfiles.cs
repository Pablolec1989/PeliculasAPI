using AutoMapper;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;

namespace PeliculasAPI.Utilidades
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            ConfigurarMapeoGeneros();
            ConfigurarMapeoActores();
        }

        private void ConfigurarMapeoGeneros()
        {
            CreateMap<GeneroCreationDTO, Genero>();
            CreateMap<Genero, GeneroDTO>();
        }

        private void ConfigurarMapeoActores()
        {
            CreateMap<ActorCreationDTO, Actor>()
                .ForMember(a => a.Foto, config => config.Ignore());
            CreateMap<Actor, ActorDTO>();
        }
    }
}
