using PeliculasAPI.Controllers;

namespace PeliculasAPI.DTOs
{
    public class PeliculaDetallesDTO : PeliculaDTO
    {
        public List<GeneroDTO> Generos { get; set; } = [];
        public List<CineDTO> Cines { get; set; } = [];
        public List<PeliculaActorDTO> Actores { get; set; } = [];
    }
}
