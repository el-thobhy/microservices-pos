using Framework.Auth;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Entities;

namespace User.Domain.Repositories
{

    public interface IUserRepository
    {
        Task<UserEntity> Login(string username, string password);
        Task<UserEntity> Add(UserEntity entity);
        Task<int> SaveChangeAsync(CancellationToken cancellationToken = default);
    }

    public class UserRepository : IUserRepository
    {
        protected readonly UserDbContext _context;
        public UserRepository(UserDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }
        public async Task<UserEntity> Add(UserEntity entity)
        {
            _context.Set<UserEntity>().Add(entity);
            return entity;
        }

        public async Task<UserEntity> Login(string username, string password)
        {
            return await _context.Set<UserEntity>()
                .FirstOrDefaultAsync(o=>o.UserName == username && o.Password == Encryption.HashSha256(password)) 
                ?? new UserEntity();
        }

        public async Task<int> SaveChangeAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
