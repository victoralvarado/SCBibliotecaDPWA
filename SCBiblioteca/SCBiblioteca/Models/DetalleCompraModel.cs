using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCBiblioteca.Models
{
    public class DetalleCompraModel
    {
        public int IdDetalleCompra { get; set; }

        public int Cantidad { get; set; }

        public double Subtotal { get; set; }

        public int IdCompra { get; set; }

        public int IdLibro { get; set; }
    }
}