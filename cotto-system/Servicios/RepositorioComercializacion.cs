using cotto_system.interfaces;
using cotto_system.Modelos;
using cotto_system.Modelos.ComercializacionModel;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace cotto_system.Servicios
{
    public class RepositorioComercializacion : IRepositorioComercializacion
    {
        private readonly string dbConnectionString;

        public RepositorioComercializacion(IConfiguration configuration)
        {
            dbConnectionString = configuration.GetConnectionString("calculacott");
        }
        public async Task<int> addCalculoCompraEnc(AddCalculocompraenc addCalculocompraenc)
        {
            using var connection = new SqlConnection(dbConnectionString);
            var parameters = new DynamicParameters();

            parameters.Add("@idcalculocompra", addCalculocompraenc.idcalculo, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            parameters.Add("@idproveedor ", addCalculocompraenc.idcliente);
            parameters.Add("@idunidadcompra ", addCalculocompraenc.idunidadventa);
            parameters.Add("@valorunidad ", addCalculocompraenc.valorunidad);
            parameters.Add("@precio ", addCalculocompraenc.precio);
            parameters.Add("@puntos ", addCalculocompraenc.puntos);
            parameters.Add("@preciosm ", addCalculocompraenc.preciosm);
            parameters.Add("@preciom ", addCalculocompraenc.preciom);
            parameters.Add("@preciomp ", addCalculocompraenc.preciomp);
            parameters.Add("@precioslmp ", addCalculocompraenc.precioslmp);
            parameters.Add("@precioslm ", addCalculocompraenc.precioslm);
            parameters.Add("@preciolmp ", addCalculocompraenc.preciolmp);
            parameters.Add("@preciolm ", addCalculocompraenc.preciolm);
            parameters.Add("@preciosgo ", addCalculocompraenc.preciosgo);
            parameters.Add("@preciogo ", addCalculocompraenc.preciogo);
            parameters.Add("@precioo ", addCalculocompraenc.precioo);
            parameters.Add("@cantidadpacas ", addCalculocompraenc.cantidadpacas);
            parameters.Add("@activatara ", addCalculocompraenc.activatara);
            parameters.Add("@valortara ", addCalculocompraenc.valortara);
            parameters.Add("@idperfilcompra ", addCalculocompraenc.idperfilventa);
            parameters.Add("@activamic ", addCalculocompraenc.activamic);
            parameters.Add("@idperfilmic ", addCalculocompraenc.idperfilmic);
            parameters.Add("@castigomic ", addCalculocompraenc.castigomic);
            parameters.Add("@activauhml ", addCalculocompraenc.activauhml);
            parameters.Add("@idperfiluhml ", addCalculocompraenc.idperfiluhml);
            parameters.Add("@castigouhml ", addCalculocompraenc.castigouhml);
            parameters.Add("@activastr ", addCalculocompraenc.activastr);
            parameters.Add("@idperfilstr ", addCalculocompraenc.idperfilstr);
            parameters.Add("@castigostr ", addCalculocompraenc.castigostr);
            parameters.Add("@activauni ", addCalculocompraenc.activauni);
            parameters.Add("@idperfiluni ", addCalculocompraenc.idperfiluni);
            parameters.Add("@castigouni ", addCalculocompraenc.castigouni);
            parameters.Add("@activasfi ", addCalculocompraenc.activasfi);
            parameters.Add("@idperfilsfi ", addCalculocompraenc.idperfilsfi);
            parameters.Add("@castigosfi ", addCalculocompraenc.castigosfi);
            parameters.Add("@subtotal ", addCalculocompraenc.subtotal);
            parameters.Add("@castigototal ", addCalculocompraenc.castigototal);
            parameters.Add("@preciototal ", addCalculocompraenc.preciototal);
            parameters.Add("@observaciones ", addCalculocompraenc.observaciones);
            parameters.Add("@idestatus ", addCalculocompraenc.idestatus);
            parameters.Add("@fechacreacion ", addCalculocompraenc.fechacreacion);
            parameters.Add("@fechaactualizacion ", addCalculocompraenc.fechaactualizacion);

            await connection.ExecuteAsync("pa_insertacalculocompraenc", parameters, commandType: System.Data.CommandType.StoredProcedure);

            return parameters.Get<int>("@idcalculocompra");
        }
        public async Task<int> addCalculoCompraDet(AddCalculocompradet addCalculocompradet)
        {
            using var connection = new SqlConnection(dbConnectionString);
            var parameters = new DynamicParameters();

            parameters.Add("@idcalculodet", addCalculocompradet.idcalculodet);
            parameters.Add("@idcalculo", addCalculocompradet.idcalculo);
            parameters.Add("@baleid", addCalculocompradet.baleid);
            parameters.Add("@kilos", addCalculocompradet.kilos);
            parameters.Add("@libras", addCalculocompradet.libras);
            parameters.Add("@quintales", addCalculocompradet.quintales);
            parameters.Add("@preciopaca", addCalculocompradet.preciopaca);
            parameters.Add("@precioclase", addCalculocompradet.precioclase);
            parameters.Add("@castigomic", addCalculocompradet.castigomic);
            parameters.Add("@castigouhml", addCalculocompradet.castigouhml);
            parameters.Add("@castigores", addCalculocompradet.castigores);
            parameters.Add("@castigouni", addCalculocompradet.castigouni);
            parameters.Add("@castigosfi", addCalculocompradet.castigosfi);

            await connection.ExecuteAsync("pa_insertacalculocompraenc", parameters, commandType: System.Data.CommandType.StoredProcedure);

            return parameters.Get<int>("@idcalculocompra");
        }
        public async Task<IEnumerable<GetCalculocompraenc>> GetCalculoCompraEnc()
        {
            using var connection = new SqlConnection(dbConnectionString);

            return await connection.QueryAsync<GetCalculocompraenc>("pa_consultacalculoscompra", commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
