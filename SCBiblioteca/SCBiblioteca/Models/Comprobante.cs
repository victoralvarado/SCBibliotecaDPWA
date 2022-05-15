namespace SCBiblioteca.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Comprobante
    {
        public int IdComprobante { get; set; }
        public string Comprobante1 { get; set; }
        public byte Activo { get; set; }
        public System.DateTime FechaCreacion { get; set; }
        public System.DateTime FechaVencimiento { get; set; }
        public int IdUsuario { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
