using cotto_system.interfaces;
using cotto_system.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using MySqlConnector;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace cotto_system.Servicios
{
    public class RepositorioUsuario : IRepositorioUsuario
    {
        private readonly string connectionString;

        public RepositorioUsuario(IConfiguration configuration)
        {
            //connectionString = configuration.GetConnectionString("defaultConnection");
            connectionString = Environment.GetEnvironmentVariable("defaultConnection");
        }

        public async Task AddUsuario(AddUsuario usuario)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);

                usuario.Clave = Encritptar(usuario.Clave);

                await connection.ExecuteAsync("sp_InsertarUsuario", usuario, commandType: System.Data.CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<GetUsuario> Login(string Usuario)
        {
            try
            {
                //using var connection = new SqlConnection(connectionString); este es para Sql Server
                using var connection = new MySqlConnection(connectionString); // Este es para MySql

                //return await connection.QueryFirstOrDefaultAsync<GetUsuario>("Sp_ConsultaUsuario", new { Usuario }, commandType: System.Data.CommandType.StoredProcedure); Este es para SQl SERVER
                return await connection.QueryFirstOrDefaultAsync<GetUsuario>("Sp_ConsultaUsuario", new { p_Usuario = Usuario }, commandType: System.Data.CommandType.StoredProcedure); //Este es para una base de prueba que se cargo de MYSQL

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public string Encritptar(string password)
        {
            using (SHA512 cifrador = SHA512.Create())
            {
                byte[] claveOriginal = Encoding.UTF8.GetBytes(password);
                byte[] claveCifrada = cifrador.ComputeHash(claveOriginal);
                return Convert.ToBase64String(claveCifrada);
            }
        }
        public bool verifyPassword(string hashPassword, string password)
        {
            if (hashPassword == Encritptar(password)) return true;

            return false;
        }
    }
}
