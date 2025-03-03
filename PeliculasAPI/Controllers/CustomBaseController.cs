using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;
using PeliculasAPI.Utilidades;
using System.Linq.Expressions;

namespace PeliculasAPI.Controllers
{
    public class CustomBaseController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CustomBaseController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        protected async Task<List<TDTO>> Get<TEntidad, TDTO>(PaginacionDTO paginacion, 
            Expression<Func<TEntidad, object>> ordenarPor)
            where TEntidad : class
        {
            var queryable = context.Set<TEntidad>().AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);

            return await queryable
                .OrderBy(ordenarPor)
                .Paginar(paginacion)
                .ProjectTo<TDTO>(mapper.ConfigurationProvider)
                .ToListAsync();

        }

        protected async Task<ActionResult<TDTO>> Get<TEntidad, TDTO> (int id)
            where TEntidad : class, IId
            where TDTO : IId
        {
            var entidad = await context.Set<TEntidad>()
                .ProjectTo<TDTO>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == id);

            if(entidad is null)
            {
                return NotFound();
            }

            return entidad;
        }
    }
}
