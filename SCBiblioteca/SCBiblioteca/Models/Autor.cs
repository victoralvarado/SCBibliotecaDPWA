namespace SCBiblioteca.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Autor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Autor()
        {
            this.Libro = new HashSet<Libro>();
        }

        public int IdAutor { get; set; }
        public string Autor1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Libro> Libro { get; set; }
    }
}
