using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ActividadEntregable1.Domain
{
    public class Articulo
    {
        public int Codigo { get; set; }

        public string Nombre { get; set; }

        public float PrecioUnitario { get; set; }

        public override string? ToString()
        {
            return "[" + Codigo + "] " + Nombre;
        }

    }
}
