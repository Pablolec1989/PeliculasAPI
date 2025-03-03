using PeliculasAPI.DTOs;

namespace PeliculasAPI.Utilidades
{
    public static class IQeryableExtensions
    {
        public static IQueryable<T> Paginar<T>(this IQueryable<T> queryable, PaginacionDTO paginacion)
        {
            return queryable.
                Skip((paginacion.Pagina - 1) * paginacion.RecordsPorPagina).
                Take(paginacion.RecordsPorPagina);
        }
    }
}
