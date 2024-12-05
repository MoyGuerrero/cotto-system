namespace cotto_system.Modelos
{
    public class AddUsuario
    {
        public string Nombre { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public int Tipo { get; set; }
        public int ClaveAutorizacion { get; set; }
        public int Estatus { get; set; }
    }
}
