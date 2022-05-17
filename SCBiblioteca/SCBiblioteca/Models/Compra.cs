namespace SCBiblioteca.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Compra
    {
        public int IdCompra { get; set; }

        [Display(Name = "Correlatvo")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public string Correlativo { get; set; }

        [Required(ErrorMessage = "La {0} es requerida.")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El {0} es requerido.")]
        public decimal Subtotal { get; set; }
        public Nullable<decimal> TotalCompra { get; set; }

        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public System.DateTime FechaCompra { get; set; }

        [Display(Name = "Libro")]
        public int IdLibro { get; set; }

        [Display(Name = "Editorial")]
        public int IdEditorial { get; set; }

        public virtual Editorial Editorial { get; set; }
        public virtual Libro Libro { get; set; }
    }
}
