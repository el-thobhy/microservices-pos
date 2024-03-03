using AutoMapper;
using Framework.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Dtos;
using User.Domain.Entities;
using User.Domain.Repositories;

namespace User.Domain.Services
{
    public interface IUserService
    {

        Task<IEnumerable<UserDto>> GetAllUsers();
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
        public async Task<UserDto> AddUser(UserDto dto)
        {
            dto.Password = Encryption.HashSha256(dto.Password);
            var dtoToEntity = _mapper.Map<UserEntity>(dto);
            var entity = await _repository.Add(dtoToEntity);
            var result = await _repository.SaveChangeAsync();

            //event driven, Event bus
            if (result > 0)
            {
                return _mapper.Map<UserDto>(entity);
            }
            return new UserDto();
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

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            return _mapper.Map<IEnumerable<UserDto>>( await _repository.GetAll());
        }
    }


}
