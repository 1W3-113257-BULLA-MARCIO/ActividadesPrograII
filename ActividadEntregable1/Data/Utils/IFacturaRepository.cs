using ActividadEntregable1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActividadEntregable1.Data.Utils
{
    public interface IFacturaRepository
    {
        List<Factura> GetAll();
        Factura GetById(int id);
        bool Save(Factura oFactura);
        bool Delete(int id);
    }
}
