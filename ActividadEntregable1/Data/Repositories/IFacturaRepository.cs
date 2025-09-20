using RepositoryEf.Data.Models;

namespace RepositoryEf.Data.Repositories
{
    public interface IFacturaRepository
    {
        void Create(Factura factura);

        void Update(Factura factura);

        void Delete(int id);

        List<Factura> GetAll();

        Factura? GetById(int id);
    }
}
