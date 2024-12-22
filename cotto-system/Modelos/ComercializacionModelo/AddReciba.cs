namespace cotto_system.Modelos.ComercializacionModelo
{
    public class Reciba:Tarjeta
    {
        public int IdReciba { get; set; }
        public int IdProveedor { get; set; }
        public int IdZona { get; set; }
        public int IdGin { get; set; }
        public int IdTemporada { get; set; }
        public string Observaciones { get; set; }
        public int cantidadpacas { get; set; }
    }
}
