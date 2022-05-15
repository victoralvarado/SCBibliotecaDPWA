namespace SCBiblioteca.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Devolucion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Devolucion()
        {
            this.Penalizacion = new HashSet<Penalizacion>();
        }

        public int IdDevolucion { get; set; }
        public System.DateTime FechaDevolucion { get; set; }
        public string Observaciones { get; set; }
        public int IdPrestamo { get; set; }

        public virtual Prestamo Prestamo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Penalizacion> Penalizacion { get; set; }
    }
}
