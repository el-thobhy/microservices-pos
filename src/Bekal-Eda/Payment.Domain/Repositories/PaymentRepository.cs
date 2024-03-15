using Microsoft.EntityFrameworkCore;
using Payment.Domain.Entity;

namespace Payment.Domain.Repositories
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<PaymentEntity>> All();
        Task<PaymentEntity> GetById(Guid id);
        Task<PaymentEntity> Update(PaymentEntity entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
    public class PaymentRepository : IPaymentRepository
    {
        protected readonly PaymentDbContext _context;
        public PaymentRepository(PaymentDbContext context)
        {
            _context = context;
        }

        public async Task<PaymentEntity> GetById(Guid id)
        {
            return await _context.Set<PaymentEntity>()
                .Include(x=>x.Customer)
                .FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<PaymentEntity> Update(PaymentEntity entity)
        {
            _context.Set<PaymentEntity>().Update(entity);
            return entity;
        }
        public async Task<IEnumerable<PaymentEntity>> All()
        {
            return await _context.Set<PaymentEntity>().Include("Customer").ToListAsync();
        }

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
