namespace SCBiblioteca.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Editorial
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Editorial()
        {
            this.Compra = new HashSet<Compra>();
        }

        public int IdEditorial { get; set; }
        public string Editorial1 { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Compra> Compra { get; set; }
    }
}
