using FluentValidation;
using FluentValidation.Results;
using Framework.Validation;
using User.Domain.Dtos;
using User.Domain.Services;

namespace User_GraphQL.Schema.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class UserMutation
    {
        private readonly IUserService _service;
        private IValidator<UserDto> _validator;
        public UserMutation(IUserService service, IValidator<UserDto> validator)
        {
            _service = service;
            _validator = validator;
        }

        public async Task<UserDto> AddUserAsync(UserDto dto)
        {
            ValidationResult resultVal = await _validator.ValidateAsync(dto);
            if(!resultVal.IsValid)
            {
                throw new GraphQLException(ValidationError.Create(resultVal));
            }
            else
            {
                var result = await _service.AddUser(dto);
                return result;
            }
        }
    }
}
