using cotto_system.interfaces;
using cotto_system.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
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
            connectionString = configuration.GetConnectionString("defaultConnection");
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
                using var connection = new SqlConnection(connectionString);

                return await connection.QueryFirstOrDefaultAsync<GetUsuario>("Sp_ConsultaUsuario", new { Usuario = Usuario }, commandType: System.Data.CommandType.StoredProcedure);

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
