namespace SCBiblioteca.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            this.Comprobante = new HashSet<Comprobante>();
            this.Solicitud = new HashSet<Solicitud>();
        }

        public int IdUsuario { get; set; }
        public string NombreCompleto { get; set; }
        public System.DateTime FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public string CorreoElectronico { get; set; }
        public string Usuario1 { get; set; }
        public string Password { get; set; }
        public int IdRol { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comprobante> Comprobante { get; set; }
        public virtual Rol Rol { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Solicitud> Solicitud { get; set; }
    }
}
