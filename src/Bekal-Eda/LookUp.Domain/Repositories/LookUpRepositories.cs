using LookUp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookUp.Domain.Repositories
{
    public interface ILookUpReposiotories
    {
        Task<IEnumerable<AttributesEntity>> GetAll();
        Task<AttributesEntity> Add(AttributesEntity entity);
        Task<int> SaveChangeAsyns(CancellationToken cancellation = default);
    }
    public class LookUpRepositories : ILookUpReposiotories
    {
        protected readonly LookUpDbContext _context;
        public LookUpRepositories(LookUpDbContext context)
        {
            _context = context;
        }
        public async Task<AttributesEntity> Add(AttributesEntity entity)
        {
            _context.Entry(entity).State = EntityState.Added;
            return entity;
        }

        public async Task<IEnumerable<AttributesEntity>> GetAll()
        {
            return await _context.Set<AttributesEntity>().ToListAsync();    
        }

        public async Task<int> SaveChangeAsyns(CancellationToken cancellation = default)
        {
            return await _context.SaveChangesAsync(cancellation);
        }
    }
}
