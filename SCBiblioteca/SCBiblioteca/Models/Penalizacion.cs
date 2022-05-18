namespace SCBiblioteca.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Penalizacion
    {
        public int IdPenalizacion { get; set; }

        [Display(Name = "Monto Penalización")]
        [Required(ErrorMessage = "El {0} es requerida.")]
        public decimal MontoPenalizacion { get; set; }

        [Display(Name = "Motivo de Penalización")]
        public string MotivoPenalizacion { get; set; }

        [Display(Name = "Devolución")]
        public Nullable<int> IdDevolucion { get; set; }

        public virtual Devolucion Devolucion { get; set; }
    }
}
