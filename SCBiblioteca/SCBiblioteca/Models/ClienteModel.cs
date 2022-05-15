using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCBiblioteca.Models
{
    public class ClienteModel
    {
        public int IdCliente { get; set; }

        public string Nombrecompleto { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string Direccion { get; set; }

        public string CorreoElectronico { get; set; }

        public string Telefono { get; set; }
    }
}