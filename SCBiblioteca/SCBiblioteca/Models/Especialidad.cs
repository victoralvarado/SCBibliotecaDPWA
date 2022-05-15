namespace SCBiblioteca.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Especialidad
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Especialidad()
        {
            this.Libro = new HashSet<Libro>();
        }

        public int IdEspecialidad { get; set; }
        public string Especialidad1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Libro> Libro { get; set; }
    }
}
