namespace PeliculasAPI.Entidades
{
    public class PeliculaCine
    {
        public int CineId { get; set; }
        public int PeliculaId { get; set; }

        //Navigation prop.
        public Cine Cine { get; set; } = null!;
        public Pelicula Pelicula { get; set; } = null!;
    }
}
