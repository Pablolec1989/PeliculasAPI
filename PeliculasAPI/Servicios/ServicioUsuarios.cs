using Microsoft.AspNetCore.Identity;

namespace PeliculasAPI.Servicios
{
    public class ServicioUsuarios : IServicioUsuarios
    {
        private readonly IHttpContextAccessor contextAccessor;
        private readonly UserManager<IdentityUser> userManager;

        public ServicioUsuarios(IHttpContextAccessor contextAccessor, UserManager<IdentityUser> userManager)
        {
            this.contextAccessor = contextAccessor;
            this.userManager = userManager;
        }

        public async Task<string> ObtenerUsuarioId()
        {
            var email = contextAccessor.HttpContext!.User.Claims.FirstOrDefault(x => x.Type == "email")!.Value;
            var usuario = await userManager.FindByEmailAsync(email);

            return usuario!.Id;
        }
    }
}
