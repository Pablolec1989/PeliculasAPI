using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;
using PeliculasAPI.Utilidades;

namespace PeliculasAPI.Controllers
{
    [ApiController]
    [Route("api/generos")]
    public class GenerosController : CustomBaseController
    {
        private readonly IOutputCacheStore outputCacheStore;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private const string cacheTag = "generos";

        public GenerosController(IOutputCacheStore outputCacheStore, 
            ApplicationDbContext context, 
            IMapper mapper) : base(context, mapper)
        {
            this.outputCacheStore = outputCacheStore;
            this.context = context;
            this.mapper = mapper;
        }



        [HttpGet(Name = "ObtenerGeneroPorId")]
        [OutputCache(Tags = [cacheTag])]
        public async Task<List<GeneroDTO>> Get([FromQuery] PaginacionDTO paginacion)
        {
            return await Get<Genero, GeneroDTO>(paginacion, ordenarPor: g => g.Nombre);

        }


        [HttpGet("{id:int}")]
        [OutputCache]
        public async Task<ActionResult<GeneroDTO>> ObtenerPorId(int id)
        {
            return await Get<Genero, GeneroDTO>(id);
        }
        

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GeneroCreationDTO generoCreationDTO)
        {
            var genero = mapper.Map<Genero>(generoCreationDTO);

            context.Add(genero);
            await context.SaveChangesAsync();
            
            await outputCacheStore.EvictByTagAsync(cacheTag, default);

            return CreatedAtRoute("ObtenerGeneroPorId", new { id = genero.Id }, genero);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] GeneroCreationDTO generoCreationDTO)
        {
            var generoExiste = await context.Generos.AnyAsync(g => g.Id == id);

            if (!generoExiste)
            {
                return NotFound();
            }

            var genero = mapper.Map<Genero>(generoCreationDTO);

            genero.Id = id;

            context.Update(genero);
            await context.SaveChangesAsync();
            await outputCacheStore.EvictByTagAsync(cacheTag, default);

            return NoContent();

        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var registrosBorrados = await context.Generos.Where(g => g.Id == id).ExecuteDeleteAsync();

            if(registrosBorrados == 0)
            {
                return NotFound();
            }

            await outputCacheStore.EvictByTagAsync(cacheTag, default);
            return NoContent();
        }

    }
}
