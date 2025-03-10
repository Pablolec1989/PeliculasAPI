using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.DTOs
{
    public class PeliculaActorDTO
    {
        public int Id { get; set; }
        [StringLength(150)]
        public string Nombre { get; set; } = null!;
        public string? Foto { get; set; }
        public string Personaje { get; set; } = null!;
    }
}
