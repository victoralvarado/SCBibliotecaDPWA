using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCBiblioteca.Models
{
    public class EditorialModel
    {
        public int IdEditorial { get; set; }

        public string Editorial { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }
    }
}