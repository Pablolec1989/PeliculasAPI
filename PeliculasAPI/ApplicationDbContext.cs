using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.Entidades;

namespace PeliculasAPI
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        //Config PrimaryKey
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Api-Fluent
            modelBuilder.Entity<PeliculaGenero>().HasKey(e => new { e.GeneroId, e.PeliculaId });
            modelBuilder.Entity<PeliculaCine>().HasKey(c => new { c.CineId, c.PeliculaId });
            modelBuilder.Entity<PeliculaActor>().HasKey(a => new { a.ActorId, a.PeliculaId });
        }


        //Tables in Database
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Actor> Actores { get; set; }
        public DbSet<Cine> Cines { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<PeliculaGenero> PeliculasGeneros { get; set; }
        public DbSet<PeliculaCine> PeliculasCines { get; set; }
        public DbSet<PeliculaActor> PeliculasActores { get; set; }
        public DbSet<Rating> RatingsPeliculas { get; set; }
}
}
