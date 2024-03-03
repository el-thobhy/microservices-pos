using User.Domain.Dtos;
using User.Domain.Services;

namespace User_GraphQL.Schema.Queries
{
    [ExtendObjectType(typeof(Query))]
    public class UserQuery
    {
        private readonly IUserService _userService;
        public UserQuery(IUserService userService)
        {
            _userService = userService;
        }

        [UsePaging] 
        public async Task<IEnumerable<UserDto>> GetAllUserAsync()
        {
            return await _userService.GetAllUsers();
        }
    }
}
