using Framework.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;

namespace Store.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryEntity>> GetAll();
        Task<CategoryEntity> Add(CategoryEntity entity);
        Task<CategoryEntity> Update(CategoryEntity entity);
        Task<CategoryEntity?> GetById(Guid id);
        Task<int> SaveChangeAsync(CancellationToken cancellation = default);
    }
    public class CategoryRepository : ICategoryRepository
    {
        protected readonly StoreDbContext _context;
        private ContextAccessor _contextAccessor;
        public CategoryRepository(StoreDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = new ContextAccessor(contextAccessor);
            _context.Database.EnsureCreated();
        }
        public async Task<CategoryEntity> Add(CategoryEntity entity)
        {
            entity.ModifiedBy = _contextAccessor.Id;
            _context.Entry(entity).State = EntityState.Added;
            return entity;
        }

        public async Task<IEnumerable<CategoryEntity>> GetAll()
        {
            return await _context.Set<CategoryEntity>()
                .Include(o => o.Products)
                .ToListAsync();
        }

        public async Task<CategoryEntity?> GetById(Guid id)
        {
            return await _context.Set<CategoryEntity>()
                .Include(o => o.Products)
                .ThenInclude(o => o.Attribute)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<int> SaveChangeAsync(CancellationToken cancellation = default)
        {
            return await _context.SaveChangesAsync(cancellation);
        }

        public async Task<CategoryEntity> Update(CategoryEntity entity)
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
