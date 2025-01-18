using cotto_system.interfaces;
using cotto_system.Servicios;

namespace cotto_system.Extensores
{
    public static class DependencyInjection
    {
        public static IServiceCollection AgregarRepositorios(this IServiceCollection services)
        {
            services.AddTransient<IRepositorioUsuario, RepositorioUsuario>();
            services.AddTransient<IRepositorioCatalogos, RepositorioCatalogos>();
            services.AddTransient<IRepositorioComercializacion, RepositorioComercializacion>();
            services.AddTransient<IRepositorioGuardarImagen, RepositorioGuardarImagenes>();
            return services;
        }
    }
}
