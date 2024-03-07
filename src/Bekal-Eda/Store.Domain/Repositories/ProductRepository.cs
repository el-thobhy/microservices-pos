using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Repositories
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
        protected readonly StoreDbContext _context;
        public ProductRepository(StoreDbContext context)
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
                .Include(o=>o.Category)
                .Include(o => o.Attribute)
                .ToListAsync();
        }

        public async Task<ProductEntity?> GetById(Guid id)
        {
            return await _context.Set<ProductEntity>()
                .Include(o=>o.Category) //lambda function untuk get
                .Include(o=>o.Attribute)
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
        //fungsi dispose untuk clear sampah yang ada, GC itu Garbage Collection
        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
