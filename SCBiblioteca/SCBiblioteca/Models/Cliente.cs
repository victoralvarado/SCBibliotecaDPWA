namespace SCBiblioteca.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Cliente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cliente()
        {
            this.Solicitud = new HashSet<Solicitud>();
        }

        public int IdCliente { get; set; }

        [Display(Name = "Nombre Completo")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string NombreCompleto { get; set; }

        [Display(Name = "Fecha de Nacimiento")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public System.DateTime FechaNacimiento { get; set; }

        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public string Direccion { get; set; }

        [Display(Name = "Correo Electrónico")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string CorreoElectronico { get; set; }

        [Display(Name = "Teléfono")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string Telefono { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Solicitud> Solicitud { get; set; }
    }
}
