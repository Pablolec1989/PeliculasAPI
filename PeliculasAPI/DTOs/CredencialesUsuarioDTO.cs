using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.DTOs
{
    public class CredencialesUsuarioDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public required string Email { get; set; }
        [Required(ErrorMessage = "El campo {1} es requerido")]
        public required string Password { get; set; }

    }
}
