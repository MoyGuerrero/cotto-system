using cotto_system.interfaces;
using cotto_system.Modelos.ComercializacionModelo;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace cotto_system.Servicios
{
    public class RepositorioComercializacion:IRepositorioComercializacion
    {
        private readonly string dbConnectionString;

        public RepositorioComercializacion(IConfiguration configuration)
        {
            dbConnectionString = configuration.GetConnectionString("calculacott");
        }
        public async Task addReciba(Reciba reciba)
        {
            using var connection = new SqlConnection(dbConnectionString);
            await connection.ExecuteAsync("pa_insertareciba", reciba, commandType: CommandType.StoredProcedure);
        }
        public async Task addCalculo(Calculo calculo)
        {
            using var connection = new SqlConnection(dbConnectionString);
            await connection.ExecuteAsync("pa_insertacalculoenc", calculo, commandType: CommandType.StoredProcedure);
        }
    }
}
