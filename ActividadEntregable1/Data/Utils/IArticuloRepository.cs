using ActividadEntregable1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActividadEntregable1.Data.Utils
{
    public interface IArticuloRepository
    {
        List<Articulo> GetAll();
        Articulo GetById(int id);
        bool Save(Articulo oArticulo);
        bool Delete(int id);
    }
}
