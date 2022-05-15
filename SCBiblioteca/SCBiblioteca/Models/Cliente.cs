namespace SCBiblioteca.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Cliente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cliente()
        {
            this.Solicitud = new HashSet<Solicitud>();
        }

        public int IdCliente { get; set; }
        public string NombreCompleto { get; set; }
        public System.DateTime FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public string CorreoElectronico { get; set; }
        public string Telefono { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Solicitud> Solicitud { get; set; }
    }
}
