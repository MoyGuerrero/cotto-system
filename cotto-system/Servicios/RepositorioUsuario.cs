using cotto_system.interfaces;
using cotto_system.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;

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
            var passHash = BCrypt.Net.BCrypt.HashPassword(usuario.Clave);

            usuario.Clave = passHash;

            using var connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync("INSERT INTO Usuarios (Nombre, Usuario,Clave,Tipo,ClaveAutorizacion,Estatus) VALUES (@Nombre,@Usuario,@Clave,@Tipo,@ClaveAutorizacion,@Estatus)", usuario);
        }


        public async Task<AddUsuario> Login(string usuario)
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryFirstOrDefaultAsync<AddUsuario>("SELECT * FROM Usuarios WHERE Usuario = @Usuario", new { usuario });

        }



    }
}
