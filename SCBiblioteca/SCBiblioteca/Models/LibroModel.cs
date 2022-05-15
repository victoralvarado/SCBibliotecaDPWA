using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCBiblioteca.Models
{
    public class LibroModel
    {
        public int IdLibro { get; set; }

        public string Titulo { get; set; }

        public int Stock { get; set; }

        public Byte Activo { get; set; }

        public int IdAutor { get; set; }

        public int IdCategoria { get; set; }

        public int IdEspecialidad { get; set; }
    }
}