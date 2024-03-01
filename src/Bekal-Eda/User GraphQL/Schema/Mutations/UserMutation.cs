using User.Domain.Dtos;
using User.Domain.Services;

namespace User_GraphQL.Schema.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class UserMutation
    {
        private readonly IUserService _service;
        public UserMutation(IUserService service)
        {
            _service = service;
        }

        public async Task<UserDto> AddUserAsync(UserDto dto)
        {
            var result = await _service.AddUser(dto);
            return result;
        }
    }
}
