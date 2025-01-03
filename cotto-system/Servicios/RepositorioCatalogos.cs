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

        public async Task addProveedor(Proveedor proveedor)
        {
            using var connection = new SqlConnection(dbConnectionString);
            await connection.ExecuteAsync("Pa_Insertacomprador", proveedor, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<int> addGradosClasificacion(AddGradosCalificacion addGradosCalificacion)
        {
            using var connection = new SqlConnection(dbConnectionString);
            var parameters = new DynamicParameters();

            parameters.Add("@Idgradosclasificacion", addGradosCalificacion.Idgradosclasificacion, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            parameters.Add("@gradocolor", addGradosCalificacion.gradocolor);
            parameters.Add("@trashid", addGradosCalificacion.trashid);
            parameters.Add("@descripcion", addGradosCalificacion.descripcion);
            parameters.Add("@idclase", addGradosCalificacion.idclase);

            await connection.ExecuteAsync("Pa_Insertagradosclasificacion", parameters, commandType: System.Data.CommandType.StoredProcedure);

            return parameters.Get<int>("@Idgradosclasificacion");
        }

        public async Task<int> addClases(AddClases addClases)
        {
            using var connection = new SqlConnection(dbConnectionString);

            var parameters = new DynamicParameters();

            parameters.Add("@idclasesenc", addClases.idclasesenc, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            parameters.Add("@idclasificacion", addClases.idclasificacion);
            parameters.Add("@clave", addClases.clave);
            parameters.Add("@Descripcion", addClases.Descripcion);

            await connection.ExecuteAsync("Pa_Insertaclasesclasificacion", parameters, commandType: System.Data.CommandType.StoredProcedure);

            return parameters.Get<int>("@idclasesenc");
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

        public async Task<int> AddPerfilVentaEnc(AddPerfilVentaEnc addPerfilVentaEnc)
        {
            using var connection = new SqlConnection(dbConnectionString);

            var parameters = new DynamicParameters();

            parameters.Add("@idperfilenc", addPerfilVentaEnc.idperfilenc, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            parameters.Add("@descripcion", addPerfilVentaEnc.descripcion);
            parameters.Add("@idestatus", addPerfilVentaEnc.idestatus);
            parameters.Add("@fechacreacion", addPerfilVentaEnc.fechacreacion);
            parameters.Add("@fechaactualizacion", addPerfilVentaEnc.fechaactualizacion);

            //parameters.Add("@idperfilenc", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await connection.ExecuteAsync("pa_insertaperfilventaenc", parameters, commandType: System.Data.CommandType.StoredProcedure);

            return parameters.Get<int>("@idperfilenc");
        }

        public async Task<List<int>> AddPerfilVentaDet(List<AddPerfilVentaDet> addPerfilVentaDets)
        {
            List<int> Id = new List<int>();

            for (int i = 0; i < addPerfilVentaDets.Count; i++)
            {
                using var connection = new SqlConnection(dbConnectionString);
                var parameters = new DynamicParameters();
                parameters.Add("@idperfildet", addPerfilVentaDets[i].idperfildet);
                parameters.Add("@idperfilenc", addPerfilVentaDets[i].idperfilenc);
                parameters.Add("@idclasesenc", addPerfilVentaDets[i].idclasesenc);
                parameters.Add("@diferencial", addPerfilVentaDets[i].diferencial);

                //parameters.Add("@idperfilenc", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await connection.ExecuteAsync("pa_insertaperfilventadet", parameters, commandType: System.Data.CommandType.StoredProcedure);
                Id.Add(parameters.Get<int>("@idperfilenc"));
            }
            return Id;
        }


        public async Task<IEnumerable<Clientes>> GetClientes(int idcliente, string nombre)
        {
            using var connection = new SqlConnection(dbConnectionString);

            return await connection.QueryAsync<Clientes>("pa_consultacliente", new { idcliente, nombre }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Clientes>> GetProveedor(int Idcomprador, string nombre)
        {
            using var connection = new SqlConnection(dbConnectionString);

            return await connection.QueryAsync<Clientes>("pa_consultacomprador", new { Idcomprador, nombre }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<PerfilMicVentaEnc>> GetPerfilMicVentaEnc(int posicion)
        {
            using var connection = new SqlConnection(dbConnectionString);
            List<string> endpont = new List<string> { "pa_consultaperfilmicventaenc", "pa_consultaperfilresventaenc", "pa_consultaperfiluniventaenc", "pa_consultaperfiluhmlventaenc" };

            return await connection.QueryAsync<PerfilMicVentaEnc>(endpont[posicion], commandType: System.Data.CommandType.StoredProcedure);
        }


        public async Task<int> AddPerfilDeducciones(AddPerfilesDeducciones addPerfilDeducciones, int posicion)
        {
            using var connection = new SqlConnection(dbConnectionString);

            var parameters = new DynamicParameters();

            parameters.Add("@idperfilenc", addPerfilDeducciones.idperfilenc, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            parameters.Add("@descripcion", addPerfilDeducciones.descripcion);
            parameters.Add("@idestatus", addPerfilDeducciones.idestatus);
            parameters.Add("@fechacreacion", addPerfilDeducciones.fechacreacion);
            parameters.Add("@fechaactualizacion", addPerfilDeducciones.fechaactualizacion);

            //parameters.Add("@idperfilenc", dbType: DbType.Int32, direction: ParameterDirection.Output);

            List<string> endpoint = new List<string>() { "pa_insertaperfilmicrestaenc", "pa_insertaperfilresventaenc", "pa_insertaperfiluniventaenc", "pa_insertaperfiluhmlventaenc" };

            await connection.ExecuteAsync(endpoint[posicion], parameters, commandType: System.Data.CommandType.StoredProcedure);

            return parameters.Get<int>("@idperfilenc");
        }


        public async Task<List<int>> AddPerfilDeduccionDet(List<AddPerfilDeduccionesDet> addPerfilDeduccionesDet, int position)
        {
            List<int> Id = new List<int>();
            List<string> endpoint = new List<string>() { "pa_insertaperfilmicventadet", "pa_insertaperfilresventadet", "pa_insertaperfiluniventadet" };
            string endpointSelect = endpoint[position];

            for (int i = 0; i < addPerfilDeduccionesDet.Count; i++)
            {
                using var connection = new SqlConnection(dbConnectionString);
                var parameters = new DynamicParameters();
                parameters.Add("@idperfildet", addPerfilDeduccionesDet[i].idperfildet);
                parameters.Add("@idperfilenc", addPerfilDeduccionesDet[i].idperfilenc);
                parameters.Add("@rango1", addPerfilDeduccionesDet[i].rango1);
                parameters.Add("@rango2", addPerfilDeduccionesDet[i].rango2);
                parameters.Add("@castigo", addPerfilDeduccionesDet[i].castigo);

                //parameters.Add("@idperfilenc", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await connection.ExecuteAsync(endpoint[position], parameters, commandType: System.Data.CommandType.StoredProcedure);
                Id.Add(parameters.Get<int>("@idperfilenc"));
            }

            return Id;
        }

        public async Task<List<int>> AddPerfilVentaUHMLDets(List<PerfilUHMLVentaDet> addPerfilVentaUHmlDets)
        {
            List<int> Id = new List<int>();

            for (int i = 0; i < addPerfilVentaUHmlDets.Count; i++)
            {
                using var connection = new SqlConnection(dbConnectionString);
                var parameters = new DynamicParameters();
                parameters.Add("@idperfildet", addPerfilVentaUHmlDets[i].idperfildet);
                parameters.Add("@idperfilenc", addPerfilVentaUHmlDets[i].idperfilenc);
                parameters.Add("@rango1", addPerfilVentaUHmlDets[i].rango1);
                parameters.Add("@rango2", addPerfilVentaUHmlDets[i].rango2);
                parameters.Add("@LenghtNDS", addPerfilVentaUHmlDets[i].lenghtNDS);
                parameters.Add("@castigo", addPerfilVentaUHmlDets[i].castigo);

                //parameters.Add("@idperfilenc", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await connection.ExecuteAsync("pa_insertaperfiluhmlventadet", parameters, commandType: System.Data.CommandType.StoredProcedure);
                Id.Add(parameters.Get<int>("@idperfilenc"));
            }

            return Id;
        }

        public async Task<IEnumerable<AddPerfilDeduccionesDet>> GetPerfillesDeduccionesDet(int idperfilenc, int position)
        {
            using var connection = new SqlConnection(dbConnectionString);
            List<string> endpont = new List<string>() { "pa_consultaperfilmicventadet", "pa_consultaperfilresventadet", "pa_consultaperfiluniventadet", };

            return await connection.QueryAsync<AddPerfilDeduccionesDet>(endpont[position], new { idperfilenc }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<PerfilUHMLVentaDet>> GetPerfilUHMLVentaDet(int idperfilenc)
        {
            using var connection = new SqlConnection(dbConnectionString);

            return await connection.QueryAsync<PerfilUHMLVentaDet>("pa_consultaperfiluhmlventadet", new { idperfilenc }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task DeletePerfil(int idperfilenc, int position)
        {
            using var connection = new SqlConnection(dbConnectionString);
            List<string> endpoint = new List<string>() { "pa_eliminarparametrosperfilmicdet", "pa_eliminarparametrosperfilresdet", "pa_eliminarparametrosperfilunidet", "pa_eliminarparametrosperfiluhmldet" };

            await connection.ExecuteAsync(endpoint[position], new { idperfilenc }, commandType: System.Data.CommandType.StoredProcedure);
        }


        public async Task DeleteGrados()
        {
            using var connection = new SqlConnection(dbConnectionString);

            await connection.ExecuteAsync("pa_eliminarparametrosgradosclasif", commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
