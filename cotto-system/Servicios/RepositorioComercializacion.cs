using cotto_system.interfaces;

namespace cotto_system.Servicios
{
    public class RepositorioComercializacion:IRepositorioComercializacion
    {
        private readonly string dbConnectionString;

        public RepositorioComercializacion(IConfiguration configuration)
        {
            dbConnectionString = configuration.GetConnectionString("calculacott");
        }
    }
}
