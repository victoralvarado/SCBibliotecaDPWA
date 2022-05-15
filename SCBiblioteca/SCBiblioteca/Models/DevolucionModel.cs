using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCBiblioteca.Models
{
    public class DevolucionModel
    {
        public int IdDevolucion { get; set; }

        public DateTime FechaDevolucion { get; set; }

        public string Observaciones { get; set; }

        public int IdPrestamo { get; set; }
    }
}