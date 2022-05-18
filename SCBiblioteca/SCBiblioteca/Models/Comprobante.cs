namespace SCBiblioteca.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Comprobante
    {
        public int IdComprobante { get; set; }

        [Display(Name = "Comprobante")]
        public string Comprobante1 { get; set; }

        public byte Activo { get; set; }

        [Display(Name = "Fecha de Creación")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public System.DateTime FechaCreacion { get; set; }

        [Display(Name = "Fecha de Vencimiento")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public System.DateTime FechaVencimiento { get; set; }

        [Display(Name = "Usuario")]
        public int IdUsuario { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
