using Microsoft.EntityFrameworkCore;
using Product.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductEntity>> GetAll();
        Task<ProductEntity> Add(ProductEntity entity);
        Task<ProductEntity> Update(ProductEntity entity);
        Task<ProductEntity?> GetById(Guid id);
        Task<int> SaveChangeAsync(CancellationToken cancellation = default);
    }
    public class ProductRepository : IProductRepository
    {
        protected readonly ProductDbContext _context;
        public ProductRepository(ProductDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public async Task<ProductEntity> Add(ProductEntity entity)
        {
            _context.Entry(entity).State = EntityState.Added;
            return entity;
        }

        public async Task<IEnumerable<ProductEntity>> GetAll()
        {
            return await _context.Set<ProductEntity>()
            .ToListAsync();
        }

        public async Task<ProductEntity?> GetById(Guid id)
        {
            return await _context.Set<ProductEntity>()
           .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<int> SaveChangeAsync(CancellationToken cancellation = default)
        {
            return await _context.SaveChangesAsync(cancellation);
        }

        public async Task<ProductEntity> Update(ProductEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }
    }

}
