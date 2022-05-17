namespace SCBiblioteca.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Prestamo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Prestamo()
        {
            this.Devolucion = new HashSet<Devolucion>();
        }

        public int IdPrestamo { get; set; }

        [Display(Name = "Fecha del Prestamo")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public System.DateTime FechaPrestamo { get; set; }
        public byte Activo { get; set; }
        public int Cantidad { get; set; }

        [Display(Name = "Solicitud")]
        public int IdSolicitud { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Devolucion> Devolucion { get; set; }
        public virtual Solicitud Solicitud { get; set; }
    }
}
