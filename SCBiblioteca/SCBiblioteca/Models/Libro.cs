namespace SCBiblioteca.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Libro
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Libro()
        {
            this.Compra = new HashSet<Compra>();
            this.Solicitud = new HashSet<Solicitud>();
        }

        public int IdLibro { get; set; }
        public string Titulo { get; set; }
        public int Stock { get; set; }
        public byte Activo { get; set; }
        public int IdAutor { get; set; }
        public Nullable<int> IdCategoria { get; set; }
        public Nullable<int> IdEspecialidad { get; set; }

        public virtual Autor Autor { get; set; }
        public virtual Categoria Categoria { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Compra> Compra { get; set; }
        public virtual Especialidad Especialidad { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Solicitud> Solicitud { get; set; }
    }
}
