namespace SCBiblioteca.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Devolucion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Devolucion()
        {
            this.Penalizacion = new HashSet<Penalizacion>();
        }

        public int IdDevolucion { get; set; }

        [Display(Name = "Fecha de devolución")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public System.DateTime FechaDevolucion { get; set; }

        [Display(Name = "Observaciones")]
        public string Observaciones { get; set; }

        [Display(Name = "Prestamo")]
        public int IdPrestamo { get; set; }

        public virtual Prestamo Prestamo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Penalizacion> Penalizacion { get; set; }
    }
}
