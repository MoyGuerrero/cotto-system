﻿using cotto_system.interfaces;
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
            List<string> endpont = new List<string> { "pa_consultaperfilmicventaenc", "pa_consultaperfilresventaenc", "pa_consultaperfilsfiventaenc", "pa_consultaperfiluhmlventaenc" };

            return await connection.QueryAsync<PerfilMicVentaEnc>(endpont[posicion], commandType: System.Data.CommandType.StoredProcedure);
        }

    }
}
