using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCBiblioteca.Models
{
    public class PenalizacionModel
    {
        public int IdPenalizacion { get; set; }

        public double MontoPenalizacion { get; set; }

        public string MotivoPenalizacion { get; set; }

        public int IdDevolucion { get; set; }
    }
}