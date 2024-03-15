using Microsoft.EntityFrameworkCore;
using Payment.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Domain.Repositories
{
    public interface ICartProductRepository
    {
        Task<IEnumerable<CartProductEntity>> GetByCartId(Guid id);
    }

    public class CartProductRepository : ICartProductRepository
    {
        protected readonly PaymentDbContext _context;

        public CartProductRepository(PaymentDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CartProductEntity>> GetByCartId(Guid id)
        {
            return await _context.Set<CartProductEntity>().Where(x => x.CartId == id).ToListAsync();
        }
    }
}