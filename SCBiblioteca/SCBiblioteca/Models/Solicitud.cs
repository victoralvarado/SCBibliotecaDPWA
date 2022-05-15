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
    
    public partial class Solicitud
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Solicitud()
        {
            this.Prestamo = new HashSet<Prestamo>();
        }
    
        public int IdSolicitud { get; set; }
        public System.DateTime FechaSolicitud { get; set; }
        public int CantidadLibros { get; set; }
        public int Activo { get; set; }
        public Nullable<int> IdCliente { get; set; }
        public int IdLibro { get; set; }
        public Nullable<int> IdUsuario { get; set; }
    
        public virtual Cliente Cliente { get; set; }
        public virtual Libro Libro { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prestamo> Prestamo { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
