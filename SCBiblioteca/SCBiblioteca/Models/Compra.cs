namespace SCBiblioteca.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Compra
    {
        public int IdCompra { get; set; }
        public string Correlativo { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }
        public Nullable<decimal> TotalCompra { get; set; }
        public System.DateTime FechaCompra { get; set; }
        public int IdLibro { get; set; }
        public int IdEditorial { get; set; }

        public virtual Editorial Editorial { get; set; }
        public virtual Libro Libro { get; set; }
    }
}
