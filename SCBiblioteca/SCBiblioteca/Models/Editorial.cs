namespace SCBiblioteca.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Editorial
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Editorial()
        {
            this.Compra = new HashSet<Compra>();
        }

        public int IdEditorial { get; set; }

        [Display(Name = "Editorial")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public string Editorial1 { get; set; }

        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public string Direccion { get; set; }

        [Display(Name = "Teléfono")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string Telefono { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Compra> Compra { get; set; }
    }
}
