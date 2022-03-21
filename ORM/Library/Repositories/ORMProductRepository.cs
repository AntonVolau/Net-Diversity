using System.Collections.Generic;
using System.Linq;
using ORMLibrary.Models;

namespace ORMLibrary.Repositories
{
    public class ORMProductRepository
    {
        private readonly ORMOrderProductContext _context;

        public ORMProductRepository()
        {
            _context = new ORMOrderProductContext();  
        }

        public void Create(ORMProduct product)
        {
            _context.Products.Add(product); 
            _context.SaveChanges();
        }

        public ORMProduct Read(int id)
        {
            return _context.Products.Find(id);  
        }

        public void Update(ORMProduct product)
        {
            var toUpdate = _context.Products.Find(product.Id);
            if (toUpdate != null)
            {
                _context.Entry(toUpdate).CurrentValues.SetValues(product);
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var toDelete = _context.Products.Find(id);
            if (toDelete != null)
            {
                _context.Products.Remove(toDelete);
                _context.SaveChanges();
            }
        }

        public IEnumerable<ORMProduct> Read()
        {
            return _context.Products.ToList();
        }

        public void Delete()
        {
            _context.Products.RemoveRange(_context.Products);
        }
    }
}
