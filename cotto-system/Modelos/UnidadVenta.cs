﻿namespace cotto_system.Modelos
{
    public class UnidadVenta
    {
        public int idperfilenc { get; set; }
        public string descripcion { get; set; }
        public float valorunidad { get; set; }
        public int idestatus { get; set; }
        public string estatus { get; set; }
        public DateTime fechacreacion { get; set; }
        public DateTime fechaactualizacion { get; set; }
    }
}
