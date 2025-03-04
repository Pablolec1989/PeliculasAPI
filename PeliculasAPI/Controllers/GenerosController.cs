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
            IMapper mapper) : base(context, mapper, outputCacheStore, cacheTag)
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
            return await Post<GeneroCreationDTO, Genero, GeneroDTO>(generoCreationDTO, "ObtenerGeneroPorId");
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] GeneroCreationDTO generoCreationDTO)
        {
            return await Put<GeneroCreationDTO, Genero>(id, generoCreationDTO);


        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await Delete<Genero>(id);
        }

    }
}
