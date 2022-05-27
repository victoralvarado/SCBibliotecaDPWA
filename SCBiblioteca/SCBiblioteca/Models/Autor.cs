namespace SCBiblioteca.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Autor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Autor()
        {
            this.Libro = new HashSet<Libro>();
        }

        public int IdAutor { get; set; }

        [Display(Name = "Autor")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        [MaxLength(50, ErrorMessage = "Máximo {1} caracteres permitidos")]
        public string Autor1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Libro> Libro { get; set; }
    }
}
