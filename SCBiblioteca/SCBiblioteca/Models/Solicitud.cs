namespace SCBiblioteca.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Solicitud
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Solicitud()
        {
            this.Prestamo = new HashSet<Prestamo>();
        }

        public int IdSolicitud { get; set; }
        [Display(Name = "Fecha de la Solicitud")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public System.DateTime FechaSolicitud { get; set; }

        [Display(Name = "Cantidad de libros")]
        [Range(1, 3, ErrorMessage = "La {0} debe ser {1} o maximo {2}")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public int CantidadLibros { get; set; }
        public int Activo { get; set; }

        [Display(Name = "Cliente")]
        public Nullable<int> IdCliente { get; set; }

        [Display(Name = "Libro")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public int IdLibro { get; set; }

        [Display(Name = "Usuario")]
        public Nullable<int> IdUsuario { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual Libro Libro { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prestamo> Prestamo { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
