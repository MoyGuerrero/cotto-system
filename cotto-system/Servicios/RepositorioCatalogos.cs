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

        public async Task<IEnumerable<GetClases>> getClases()
        {
            using var connection = new SqlConnection(dbConnectionString);

            return await connection.QueryAsync<GetClases>("pa_consultaclasesclasificacion", commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<PerfilVentaEnc>> getPerfilVentaEnc()
        {
            using var connection = new SqlConnection(dbConnectionString);

            return await connection.QueryAsync<PerfilVentaEnc>("pa_consultaperfilventaenc", commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<PerfilVentaDet>> getPerfilVentaDet(int idperfilenc)
        {
            using var connection = new SqlConnection(dbConnectionString);

            return await connection.QueryAsync<PerfilVentaDet>("pa_consultaprfilventadet", new { idperfilenc }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<UnidadVenta>> getUnidadVenta()
        {
            using var connection = new SqlConnection(dbConnectionString);

            return await connection.QueryAsync<UnidadVenta>("pa_consultaunidadventa", commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<int> AddValorUnidad(AddVentaUnidad addVentaUnidad)
        {
            using var connection = new SqlConnection(dbConnectionString);

            var parameters = new DynamicParameters();

            parameters.Add("@descripcion", addVentaUnidad.descripcion);
            parameters.Add("@valorunidad", addVentaUnidad.valorunidad);
            parameters.Add("@idestatus", addVentaUnidad.idestatus);
            parameters.Add("@fechacreacion", addVentaUnidad.fechacreacion);
            parameters.Add("@fechaactualizacion", addVentaUnidad.fechaactualizacion);

            parameters.Add("@idperfilenc", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await connection.ExecuteAsync("pa_insertaunidadventaenc", parameters, commandType: System.Data.CommandType.StoredProcedure);

            return parameters.Get<int>("@idperfilenc");
        }

    }
}
