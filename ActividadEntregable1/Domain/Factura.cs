using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActividadEntregable1.Domain
{
    public class Factura
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public int FormaPagoId { get; set; }

        public string Cliente { get; set; }

        private List<DetalleFactura> detalles;

        public List<DetalleFactura> GetDetails()
        {
            return detalles;
        }

        public Factura()
        {
            detalles = new List<DetalleFactura>();
        }

        public void AddDetail(DetalleFactura detail)
        {
            if (detail != null)
                detalles.Add(detail);
        }

        public void Remove(int index)
        {
            detalles.RemoveAt(index);
        }

        public float Total()
        {
            float total = 0;
            foreach (var detail in detalles)
                total += detail.SubTotal();

            return total;
        }
    }
}
