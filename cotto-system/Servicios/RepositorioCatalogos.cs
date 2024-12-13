using cotto_system.interfaces;
using cotto_system.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace cotto_system.Servicios
{
    public class RepositorioCatalogos : IRepositorioCatalogos
    {
        private readonly string dbConnectionString;

        public RepositorioCatalogos(IConfiguration configuration)
        {
            dbConnectionString = configuration.GetConnectionString("calculacott");
        }



        public async Task addClient(Clientes clientes)
        {
            using var connection = new SqlConnection(dbConnectionString);

            await connection.ExecuteAsync("Pa_InsertaCliente", clientes, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<getGradosCalificacion>> getGradosClasificacion()
        {
            using var connection = new SqlConnection(dbConnectionString);

            return await connection.QueryAsync<getGradosCalificacion>("pa_consultagradosclasificacion", commandType: System.Data.CommandType.StoredProcedure);
        }

    }
}
