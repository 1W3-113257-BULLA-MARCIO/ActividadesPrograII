using ActividadEntregable1.Data.Utils;
using ActividadEntregable1.Domain;
using RepositoryExample.Data;
using RepositoryExample.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActividadEntregable1.Services
{
    public class FacturaManager
    {
        private IFacturaRepository _repository;

        public FacturaManager()
        {
            _repository = new FacturaRepository();
        }

        public List<Factura> GetBudgets()
        {
            return _repository.GetAll();
        }

        public Factura? GetBudgetsById(int id)
        {
            return _repository.GetById(id);
        }


    }
}
