using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActividadEntregable1.Domain
{
    public class DetalleFactura
    {
        
        public Articulo Articulo { get; set; }

        public int Cantidad { get; set; }

        public float PrecioUnitario { get; set; }


        public float SubTotal()
        {
            return PrecioUnitario * Cantidad;
        }



    }
}
