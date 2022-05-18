namespace SCBiblioteca.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Penalizacion
    {
        public int IdPenalizacion { get; set; }

        [Display(Name = "Monto Penalizaci�n")]
        [Required(ErrorMessage = "El {0} es requerida.")]
        public decimal MontoPenalizacion { get; set; }

        [Display(Name = "Motivo de Penalizaci�n")]
        public string MotivoPenalizacion { get; set; }

        [Display(Name = "Devoluci�n")]
        public Nullable<int> IdDevolucion { get; set; }

        public virtual Devolucion Devolucion { get; set; }
    }
}
