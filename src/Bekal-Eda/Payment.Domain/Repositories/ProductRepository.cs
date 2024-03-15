using Payment.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<ProductEntity> GetById(Guid id);
        Task<ProductEntity> Update(ProductEntity entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

    public class ProductRepository : IProductRepository
    {
        protected readonly PaymentDbContext _context;
        public ProductRepository(PaymentDbContext context)
        {
            _context = context;
        }
        public async Task<ProductEntity> GetById(Guid id)
        {
            return await _context.Set<ProductEntity>().FindAsync(id);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<ProductEntity> Update(ProductEntity entity)
        {
            _context.Set<ProductEntity>().Update(entity);
            return entity;
        }
    }
}

