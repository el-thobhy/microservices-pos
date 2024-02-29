using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Dtos;
using User.Domain.Repositories;

namespace User.Domain.Services
{
    public interface IUserService
    {
        Task<LoginDto> Login(string username, string password);
        Task<UserDto> AddUser(UserDto userDto);
    }

    public class UserServices : IUserService
    {
        private IUserRepository _repository;
        private readonly IMapper _mapper;
        public UserServices(IUserRepository repository, IMapper mapper)
        {
            _repository= repository;
            _mapper= mapper;
        }
        public Task<UserDto> AddUser(UserDto userDto)
        {
            throw new NotImplementedException();
        }

        public async Task<LoginDto> Login(string username, string password)
        {
            var entity = await _repository.Login(username, password);
            if(entity != null)
            {
                LoginDto dto = _mapper.Map<LoginDto>(entity);
                dto.Roles.Add(entity.Type.ToString());
                return dto;
            }
            return null;
        }
    }


}
