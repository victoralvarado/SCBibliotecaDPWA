//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

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
