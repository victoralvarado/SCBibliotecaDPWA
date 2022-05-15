using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCBiblioteca.Models
{
    public class DetalleSolicitudModel
    {
        public int IdDetalleSolicitud { get; set; }

        public int Cantidad { get; set; }

        public int IdLibro { get; set; }

        public int IdSolicitud { get; set; }
    }
}