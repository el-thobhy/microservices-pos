using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities;

namespace Order.Domain.Repositories
{
    public interface ICartProductRepository
    {
        Task<CartProductEntity> Add(CartProductEntity entity);
        Task<IEnumerable<CartProductEntity>> GetAll();
        Task<CartProductEntity> GetById(Guid id);
        Task<ProductEntity> GetProductById(Guid id);
        Task<CartProductEntity> Update(CartProductEntity entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<CartProductEntity>> GetByCartId(Guid id);
        Task<CartProductEntity> GetCartById(Guid id, Guid productId);
    }
    public class CartProductRepository : ICartProductRepository
    {
        protected readonly OrderDbContext _context;
        public CartProductRepository(OrderDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }
        public async Task<CartProductEntity> Add(CartProductEntity entity)
        {
            _context.Set<CartProductEntity>().Add(entity);
            return entity;
        }

        public async Task<IEnumerable<CartProductEntity>> GetAll()
        {
            return await _context.Set<CartProductEntity>().ToListAsync();
        }

        public async Task<CartProductEntity> GetById(Guid id)
        {
            return await _context.Set<CartProductEntity>()
                .Include(x=>x.Product)
                .Include(x=>x.Cart).Include(x=>x.Cart.Customer)
                .FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<ProductEntity> GetProductById(Guid id)
        {
            return await _context.Set<ProductEntity>().FindAsync(id);
        }

        public async Task<CartProductEntity> GetCartById(Guid id, Guid productId)
        {
            return await _context.Set<CartProductEntity>().Where(o => o.CartId == id && o.ProductId == productId).FirstOrDefaultAsync();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<CartProductEntity> Update(CartProductEntity entity)
        {
            _context.Set<CartProductEntity>().Update(entity);
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

        public async Task<IEnumerable<CartProductEntity>> GetByCartId(Guid id)
        {
            return await _context.Set<CartProductEntity>().Where(o => o.CartId == id).ToListAsync();
        }
    }
}
