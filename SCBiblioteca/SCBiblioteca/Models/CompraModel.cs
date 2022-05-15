using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCBiblioteca.Models
{
    public class CompraModel
    {
        public int IdCompra { get; set; }

        public string Correlativo { get; set; }

        public DateTime FechaCompra { get; set; }

        public double TotalCompra { get; set; }

        public int IdEditorial { get; set; }
    }
}