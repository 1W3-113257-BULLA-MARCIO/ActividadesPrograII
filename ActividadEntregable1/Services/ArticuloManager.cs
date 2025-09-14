using ActividadEntregable1.Data.Utils;
using ActividadEntregable1.Domain;
using RepositoryExample.Data;
using RepositoryExample.Domain;

namespace ActividadEntregable1.Services
{
    public class ArticuloManager
    {
        private IArticuloRepository _repositorio;

        public ArticuloManager()
        {
            _repositorio = new ArticuloRepositoryAdo();
        }

        public List<Articulo> GetProducts()
        {
            return _repositorio.GetAll();
        }

        public Articulo GetProductByCodigo(int cod)
        {
            return _repositorio.GetById(cod);
        }

        public bool SaveProduct(Articulo oArticulo)
        {
            return _repositorio.Save(oArticulo);
        }
        public bool DeleteProduct(int cod)
        {
            return _repositorio.Delete(cod);
        }
    }
}
