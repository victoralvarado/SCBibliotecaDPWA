namespace SCBiblioteca.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            this.Comprobante = new HashSet<Comprobante>();
            this.Solicitud = new HashSet<Solicitud>();
        }

        public int IdUsuario { get; set; }

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
        [Required(ErrorMessage = "La {0} es requerida.")]
        public string CorreoElectronico { get; set; }

        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string Usuario1 { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public string Password { get; set; }

        [Display(Name = "Rol")]
        public int IdRol { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comprobante> Comprobante { get; set; }
        public virtual Rol Rol { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Solicitud> Solicitud { get; set; }
    }
}
