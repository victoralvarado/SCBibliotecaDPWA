using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCBiblioteca.Models
{
    public class SolicitudModel
    {
        public int Solicitud { get; set; }

        public DateTime FechaSolicitud { get; set; }

        public int CantidadLibros { get; set; }

        public Byte Activo { get; set; }

        public int IdCliente { get; set; }

        public int IdUsuario { get; set; }
    }
}