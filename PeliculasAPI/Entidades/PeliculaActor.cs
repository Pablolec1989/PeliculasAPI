using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Entidades
{
    public class PeliculaActor
    {
        public int ActorId { get; set; }
        public int PeliculaId { get; set; }
        [Required(ErrorMessage = "Debes indicar el nombre del personaje")]
        [StringLength(300)]
        public required string Personaje { get; set; }
        public int Orden { get; set; }

        //Nav. Prop
        public Actor Actor { get; set; } = null!;
        public Pelicula Pelicula { get; set; } = null!;


    }
}
