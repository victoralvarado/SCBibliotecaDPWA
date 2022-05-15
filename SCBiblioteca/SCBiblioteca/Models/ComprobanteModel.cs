using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCBiblioteca.Models
{
    public class ComprobanteModel
    {
        public int IdComprobante { get; set; }

        public string Comprobante { get; set; }

        public Byte Activo { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaVencimiento { get; set; }

        public int IdUsuario { get; set; }
    }
}