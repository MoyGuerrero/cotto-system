namespace cotto_system.Modelos.ComercializacionModel
{
    public class AddCalculocompraenc
    {
        public int idcalculo { get; set; }        
        public int idcliente { get; set; }
        public int idcomprador { get; set; }
        public int idunidadventa { get; set; }
        public decimal valorunidad { get; set; }
        public decimal precio { get; set; }
        public decimal puntos { get; set; }
        public decimal preciosm { get; set; }
        public decimal preciom { get; set; }
        public decimal preciomp { get; set; }
        public decimal precioslmp { get; set; }
        public decimal precioslm { get; set; }
        public decimal preciolmp { get; set; }
        public decimal preciolm { get; set; }
        public decimal preciosgo { get; set; }
        public decimal preciogo { get; set; }
        public decimal precioo { get; set; }
        public int cantidadpacas { get; set; }
        public bool activatara { get; set; }
        public decimal valortara { get; set; }
        public int idperfilventa { get; set; }
        public bool activamic { get; set; }
        public int idperfilmic { get; set; }
        public decimal castigomic { get; set; }
        public bool activauhml { get; set; }
        public int idperfiluhml { get; set; }
        public decimal castigouhml { get; set; }
        public bool activastr { get; set; }
        public int idperfilstr { get; set; }
        public decimal castigostr { get; set; }
        public bool activauni { get; set; }
        public int idperfiluni { get; set; }
        public decimal castigouni { get; set; }
        public bool activasfi { get; set; }
        public int idperfilsfi { get; set; }
        public decimal castigosfi { get; set; }
        public decimal subtotal { get; set; }
        public decimal castigototal { get; set; }
        public decimal preciototal { get; set; }
        public string observaciones { get; set; }
        public string nombrecomprador { get; set; }
        public string nombrecliente { get; set; }
        public decimal parametro { get; set; }
    }
}
