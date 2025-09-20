using RepositoryEf.Data.Models;

namespace RepositoryEf.Data.Repositories
{
    public class FacturaRepository : IFacturaRepository
    {
        private FactDbContext _dbContext;

        public FacturaRepository(FactDbContext context)
        {
            _dbContext = context;
        }
        public void Create(Factura factura)
        {
            _dbContext.Facturas.Add(factura);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var facturaDeleted = GetById(id);
            if (facturaDeleted != null)
            {
                _dbContext.Facturas.Remove(facturaDeleted);
                _dbContext.SaveChanges();
            }
        }

        public List<Factura> GetAll()
        {
            return _dbContext.Facturas.ToList();
        }

        public Factura? GetById(int id)
        {
            return _dbContext.Facturas.Find(id);
        }

        public void Update(Factura factura)
        {
           
            if (factura != null)
            {
                _dbContext.Facturas.Update(factura);
                _dbContext.SaveChanges();
            }
        }
    }
}
