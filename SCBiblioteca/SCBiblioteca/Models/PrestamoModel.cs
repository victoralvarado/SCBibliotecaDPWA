using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCBiblioteca.Models
{
    public class PrestamoModel
    {
        public int IdPrestamo { get; set; }

        public DateTime FechaPrestamo { get; set; }

        public Byte Activo { get; set; }

        public int IdDetalleDevolucion { get; set; }
    }
}