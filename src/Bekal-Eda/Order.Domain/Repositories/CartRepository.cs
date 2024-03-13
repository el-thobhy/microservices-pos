using Framework.Auth;
using Framework.Core.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.Repositories
{
    public interface ICartRepository
    {
        Task<CartEntity> Add(CartEntity entity);
        Task<CartEntity> Update(CartEntity entity);
        Task<CartEntity> GetById(Guid id);
        Task<IEnumerable<CartEntity>> GetAll();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
    public class CartRepository : ICartRepository
    {
        protected readonly OrderDbContext _context;
        private ContextAccessor _contextAccessor;
        public CartRepository(OrderDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = new ContextAccessor(contextAccessor);
            _context.Database.EnsureCreated();
        }
        public async Task<CartEntity> Add(CartEntity entity)
        {
            entity.ModifiedBy = _contextAccessor.Id;
            _context.Entry(entity).State = EntityState.Added;
            return entity;
        }

        public async Task<IEnumerable<CartEntity>> GetAll()
        {
            return await _context.Set<CartEntity>()
                .Include(x=>x.Customer)
                .Include(x=>x.CartProducts)
                .Where(x => !x.Status.Equals(CartStatusEnum.Removed)).ToListAsync();
        }

        public async Task<CartEntity?> GetById(Guid id)
        {
            return await _context.Set<CartEntity>()
                .Include(x=>x.Customer)
                .Include(x=>x.CartProducts)
                .FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<CartEntity> Update(CartEntity entity)
        {
            _context.Set<CartEntity>().Update(entity);
            return entity;
        }
        public virtual void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
        }
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
