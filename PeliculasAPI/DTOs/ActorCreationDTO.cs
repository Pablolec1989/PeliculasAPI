using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.DTOs
{
    public class ActorCreationDTO
    {
        [Required]
        [StringLength(150)]
        public required string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public IFormFile? Foto { get; set; }
    }
}
