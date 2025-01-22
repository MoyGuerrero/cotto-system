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

            await connection.ExecuteAsync("pa_insertacompracalculodet", parameters, commandType: System.Data.CommandType.StoredProcedure);

            return parameters.Get<int>("@idcalculocompra");
        }
        public async Task<IEnumerable<GetCalculocompraenc>> GetCalculoCompraEnc(string nombre)
        {
            using var connection = new SqlConnection(dbConnectionString);

            return await connection.QueryAsync<GetCalculocompraenc>("pa_consultacalculoscompra",new {nombre}, commandType: System.Data.CommandType.StoredProcedure);
        }
        public async Task<IEnumerable<GetPacasSinCompra>> GetPacasSinCompra(int idcliente)
        {
            using var connection = new SqlConnection(dbConnectionString);

            return await connection.QueryAsync<GetPacasSinCompra>("pa_consultapacassincompra", new { idcliente }, commandType: System.Data.CommandType.StoredProcedure);
        }
        public async Task<IEnumerable<GetPacasConCompra>> GetPacasConCompra(int idcompraenc)
        {
            using var connection = new SqlConnection(dbConnectionString);

            return await connection.QueryAsync<GetPacasConCompra>("pa_consultapacascompradet", new { idcompraenc }, commandType: System.Data.CommandType.StoredProcedure);
        }
        //CALCULO DE PACAS 
        //private void recorrepacas(int idunidad, decimal valorunidad)
        //{
        //    int decimalesDeseados = 4;
        //    decimal factor = (decimal)Math.Pow(10, decimalesDeseados);
        //    decimal sumaprecio = 0, sumacastigomic = 0, sumacastigouhml = 0, sumacastigores = 0, sumacastigouni = 0, sumacastigosfi = 0, sumacastigopredio = 0, sumacastigocamino = 0;
        //    if (idunidad == 1)
        //    {
        //        foreach (DataRow fila in dtdestino.Rows)
        //        {
        //            decimal precioclasegrade = precioclase(fila["grade"].ToString());
        //            decimal quintales = (decimal)fila["quintalescompra"];
        //            decimal kilos = Convert.ToDecimal(fila["kiloscompra"]);
        //            decimal libras = Convert.ToDecimal(fila["librascompra"]);
        //            fila["idcalculocompra"] = String.IsNullOrEmpty(tbidcalculo.Text) ? 0 : Convert.ToInt32(tbidcalculo.Text.Trim());
        //            fila["preciocompra"] = Math.Truncate((quintales * precioclasegrade) * factor) / factor;
        //            fila["precioclasecompra"] = precioclasegrade;
        //            fila["castigomiccompra"] = consultacastigomic(quintales, Math.Truncate(Convert.ToDecimal(fila["mic"]) * 100) / 100);
        //            fila["castigouhmlcompra"] = consultacastigouhml(quintales, Math.Truncate(Convert.ToDecimal(fila["uhml"]) * 100) / 100);
        //            fila["castigorescompra"] = consultacastigores(quintales, Math.Truncate(Convert.ToDecimal(fila["strength"]) * 100) / 100);
        //            fila["castigounicompra"] = consultacastigouni(quintales, Math.Truncate(Convert.ToDecimal(fila["ui"]) * 100) / 100);
        //            fila["castigosficompra"] = consultacastigosfi(quintales, Math.Truncate(Convert.ToDecimal(fila["sfi"]) * 100) / 100);

        //            if (dgvadicionales.Rows.Count > 0)
        //            {
        //                sumacastigopredio += Convert.ToBoolean(fila["activadeduccion"]) == true ? -Convert.ToDecimal(dgvadicionales.Rows[0].Cells["deduccion"].Value) : 0;
        //                sumacastigocamino += -Convert.ToDecimal(dgvadicionales.Rows[1].Cells["deduccion"].Value);
        //            }
        //            else
        //            {
        //                sumacastigopredio = 0;
        //                sumacastigocamino = 0;
        //            }
        //            sumaprecio += Math.Round((decimal)fila["preciocompra"], 5);
        //            sumacastigomic += Math.Round((decimal)fila["castigomiccompra"], 5);
        //            sumacastigouhml += Math.Round((decimal)fila["castigouhmlcompra"], 5);
        //            sumacastigores += Math.Round((decimal)fila["castigorescompra"], 5);
        //            sumacastigouni += Math.Round((decimal)fila["castigounicompra"], 5);
        //            sumacastigosfi += Math.Round((decimal)fila["castigosficompra"], 5);
        //        }
        //    }
        //    else if (idunidad == 2)
        //    {
        //        foreach (DataRow fila in dtdestino.Rows)
        //        {
        //            decimal precioclasegrade = precioclase(fila["grade"].ToString()) / 100;
        //            decimal kilos = Convert.ToDecimal(fila["kiloscompra"]);
        //            decimal libras = Convert.ToDecimal(fila["librascompra"]);
        //            fila["idcalculocompra"] = String.IsNullOrEmpty(tbidcalculo.Text) ? 0 : Convert.ToInt32(tbidcalculo.Text.Trim());
        //            fila["preciocompra"] = Math.Truncate((libras * precioclasegrade) * factor) / factor;
        //            fila["precioclasecompra"] = precioclasegrade;
        //            fila["castigomiccompra"] = (consultacastigomic(libras, Math.Truncate(Convert.ToDecimal(fila["mic"]) * 100) / 100) / 100);
        //            fila["castigouhmlcompra"] = (consultacastigouhml(libras, Math.Truncate(Convert.ToDecimal(fila["uhml"]) * 100) / 100) / 100);
        //            fila["castigorescompra"] = (consultacastigores(libras, Math.Truncate(Convert.ToDecimal(fila["strength"]) * 100) / 100) / 100);
        //            fila["castigounicompra"] = (consultacastigouni(libras, Math.Truncate(Convert.ToDecimal(fila["ui"]) * 100) / 100) / 100);
        //            fila["castigosficompra"] = (consultacastigosfi(libras, Math.Truncate(Convert.ToDecimal(fila["sfi"]) * 100) / 100) / 100);

        //            if (dgvadicionales.Rows.Count > 0)
        //            {
        //                sumacastigopredio += Convert.ToBoolean(fila["activadeduccion"]) == true ? -Convert.ToDecimal(dgvadicionales.Rows[0].Cells["deduccion"].Value) : 0;
        //                sumacastigocamino += -Convert.ToDecimal(dgvadicionales.Rows[1].Cells["deduccion"].Value);
        //            }
        //            else
        //            {
        //                sumacastigopredio = 0;
        //                sumacastigocamino = 0;
        //            }
        //            sumaprecio += Math.Round((decimal)fila["preciocompra"], 5);
        //            sumacastigomic += Math.Round((decimal)fila["castigomiccompra"], 5);
        //            sumacastigouhml += Math.Round((decimal)fila["castigouhmlcompra"], 5);
        //            sumacastigores += Math.Round((decimal)fila["castigorescompra"], 5);
        //            sumacastigouni += Math.Round((decimal)fila["castigounicompra"], 5);
        //            sumacastigosfi += Math.Round((decimal)fila["castigosficompra"], 5);
        //        }
        //    }
        //    if (dgvadicionales.Rows.Count > 0)
        //    {
        //        dgvadicionales.Rows[0].Cells["Totaldeduccion"].Value = sumacastigopredio;
        //        dgvadicionales.Rows[1].Cells["Totaldeduccion"].Value = sumacastigocamino;
        //    }
        //    nusubtotal.Value = sumaprecio;
        //    nucastigomic.Value = sumacastigomic;
        //    nucastigosfi.Value = sumacastigosfi;
        //    nucastigostr.Value = sumacastigores;
        //    nucastigouhml.Value = sumacastigouhml;
        //    nucastigouni.Value = sumacastigouni;
        //    dataGridViewDestino.Refresh();
        //    nutotaldeduccion.Value = sumacastigomic + sumacastigores + sumacastigosfi + sumacastigouhml + sumacastigouni + sumacastigopredio + sumacastigocamino;
        //    nutotal.Value = sumaprecio + sumacastigomic + sumacastigores + sumacastigosfi + sumacastigouhml + sumacastigouni + sumacastigopredio + sumacastigocamino;
        //}
        //OBTENER PRECIO DE PACA POR CLASE
        //private decimal precioclase(string grado)
        //{
        //    decimal precioencontrado = 0.0m;
        //    foreach (DataGridViewRow fila in dgvprecioclase.Rows)
        //    {
        //        if (fila.Cells["grade"].Value.ToString() == grado)
        //        {
        //            // Coincidencia encontrada, obtén el valor de la columna "Precio"
        //            if (fila.Cells["precioclase"].Value != null && decimal.TryParse(fila.Cells["precioclase"].Value.ToString(), out precioencontrado))
        //            {
        //                // Valor de "Precio" válido encontrado, puedes romper el bucle
        //                break;
        //            }
        //        }
        //    }
        //    return precioencontrado;
        //}
        //DEVOLVER PACAS
        //private void regresarpacas()
        //{
        //    dataGridViewDestino.EndEdit();
        //    foreach (DataRowView rowView in destinoView)
        //    {
        //        bool seleccionado = (bool)rowView["seleccionar"];
        //        if (seleccionado)
        //        {
        //            rowView["seleccionar"] = false;
        //            rowView["kiloscompra"] = 0M;
        //            rowView["librascompra"] = 0M;
        //            rowView["quintalescompra"] = 0M;
        //            rowView["preciocompra"] = 0M;
        //            rowView["precioclasecompra"] = 0M;
        //            rowView["castigomiccompra"] = 0M;
        //            rowView["castigouhmlcompra"] = 0M;
        //            rowView["castigorescompra"] = 0M;
        //            rowView["castigounicompra"] = 0M;
        //            rowView["castigosficompra"] = 0M;
        //            DataRow rowDestino = rowView.Row;
        //            dtorigen.ImportRow(rowDestino);
        //            rowView.Delete();
        //        }
        //    }
        //    destinoView.Table.AcceptChanges();
        //    origenView = new DataView(dtorigen);
        //    destinoView = new DataView(dtdestino);
        //    origenView.Sort = "Baleid ASC";
        //    destinoView.Sort = "Baleid ASC";
        //    registrosCargadosDestino = 0;
        //    registrosCargadosOrigen = 0;
        //    dataGridViewOrigen.RowCount = Math.Min(RegistrosPorCarga, origenView.Count - registrosCargadosOrigen);
        //    dataGridViewDestino.RowCount = Math.Min(RegistrosPorCarga, destinoView.Count - registrosCargadosDestino);
        //    tspacasseleccionadas.Text = "";
        //    tbcantidadsel2.Text = string.Empty;
        //    dataGridViewOrigen.Refresh();
        //    dataGridViewDestino.Refresh();
        //    //nutotalkilos.Value = dtdestino.AsEnumerable().Sum(row => row.Field<decimal>("kilos"));
        //    nutotalpacas.Value = dtdestino.Rows.Count;
        //    //tabpacas.SelectedIndex = 1;
        //}
    }
}
