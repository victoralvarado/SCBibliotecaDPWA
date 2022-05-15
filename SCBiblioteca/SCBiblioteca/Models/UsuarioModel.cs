using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCBiblioteca.Models
{
    public class UsuarioModel
    {
        public int IdUsuario { get; set; }

        public string NombreCompleto { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string Direccion { get; set; }

        public string CorreoElectronico { get; set; }

        public string Usuario { get; set; }

        public string Password { get; set; }

        public int IdRol { get; set; }
    }
}