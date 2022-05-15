namespace SCBiblioteca.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Prestamo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Prestamo()
        {
            this.Devolucion = new HashSet<Devolucion>();
        }

        public int IdPrestamo { get; set; }
        public System.DateTime FechaPrestamo { get; set; }
        public byte Activo { get; set; }
        public int Cantidad { get; set; }
        public int IdSolicitud { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Devolucion> Devolucion { get; set; }
        public virtual Solicitud Solicitud { get; set; }
    }
}
