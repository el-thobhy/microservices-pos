using LookUp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookUp.Domain.Repositories
{
    public interface IAttributeReposiotories
    {
        Task<IEnumerable<AttributesEntity>> GetAll();
        Task<AttributesEntity> Add(AttributesEntity entity);
        Task<AttributesEntity> Update(AttributesEntity entity);
        Task<AttributesEntity?> GetById(Guid id);
        Task<int> SaveChangeAsync(CancellationToken cancellation = default);
    }
    public class AttributeRepositories : IAttributeReposiotories
    {
        protected readonly LookUpDbContext _context;
        public AttributeRepositories(LookUpDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
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

        public async Task<AttributesEntity?> GetById(Guid id)
        {
            return await _context.Set<AttributesEntity>().FindAsync(id);
        }

        public async Task<int> SaveChangeAsync(CancellationToken cancellation = default)
        {
            return await _context.SaveChangesAsync(cancellation);
        }

        public async Task<AttributesEntity> Update(AttributesEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }


        //fungsi dispose untuk clear sampah yang ada, GC itu Garbage Collection
        public virtual void Dispose(bool disposing)
        {
            if(disposing)
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
