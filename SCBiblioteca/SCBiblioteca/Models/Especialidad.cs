namespace SCBiblioteca.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Especialidad
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Especialidad()
        {
            this.Libro = new HashSet<Libro>();
        }

        public int IdEspecialidad { get; set; }

        [Display(Name = "Especialidad")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public string Especialidad1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Libro> Libro { get; set; }
    }
}
