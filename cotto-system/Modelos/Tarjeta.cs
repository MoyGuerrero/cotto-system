using System.Data;

namespace cotto_system.Modelos
{
    public class Tarjeta
    {
        public DataTable TablaConsulta { get; set; }
        public DataTable TablaGeneral { get; set; }
        public DataTable TablaOpciones { get; set; }
        public DateTime fechacreacion { get; set; }
        public DateTime fechaactualizacion { get; set; }
    }
}
