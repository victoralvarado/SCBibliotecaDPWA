namespace SCBiblioteca.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Penalizacion
    {
        public int IdPenalizacion { get; set; }
        public decimal MontoPenalizacion { get; set; }
        public string MotivoPenalizacion { get; set; }
        public Nullable<int> IdDevolucion { get; set; }

        public virtual Devolucion Devolucion { get; set; }
    }
}
