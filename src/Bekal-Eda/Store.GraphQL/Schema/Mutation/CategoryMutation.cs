using FluentValidation;
using FluentValidation.Results;
using Framework.Auth;
using Framework.Validation;
using HotChocolate.Authorization;
using Store.Domain.Dtos;
using Store.Domain.Services;

namespace Store.GraphQL.Schema.Mutation
{
    [ExtendObjectType(typeof(Mutation))]
    public class CategoryMutation
    {
        private readonly ICategoryService _service;
        private IValidator<CategoryInputDto> _validator;
        private IValidator<CategoryStatusDto> _validatorChangeStatus;
        public CategoryMutation(ICategoryService service,IValidator<CategoryInputDto> validator, IValidator<CategoryStatusDto> validatorStatus)
        {
            _service = service;
            _validator = validator;
            _validatorChangeStatus = validatorStatus;
        }
        [Authorize(Roles = new[] { "administrator" })]
        //[ReadableBodyStream(Roles = "Administrator, customer")]
        public async Task<CategoryDto> AddAsync(CategoryInputDto dto)
        {
            ValidationResult resultVal = await _validator.ValidateAsync(dto);
            if(!resultVal.IsValid)
            {
                throw new GraphQLException(ValidationError.Create(resultVal));
            }
            else
            {
                var result = await _service.Add(dto);
                return result;
            }
        }

        [Authorize(Roles = new[] { "administrator" })]
        public async Task<CategoryDto> Update(CategoryInputDto dto)
        {
            ValidationResult resultVal = await _validator.ValidateAsync(dto);
            if (!resultVal.IsValid)
            {
                throw new GraphQLException(ValidationError.Create(resultVal));
            }
            else
            {
                var result = await _service.Updates(dto);
                if (result != null) return result;
            }
            return null;
        }

        [Authorize(Roles = new[] { "administrator" })]
        public async Task<CategoryDto> ChangeStatus(CategoryStatusDto dto)
        {
            ValidationResult resultVal = await _validatorChangeStatus.ValidateAsync(dto);
            if(!resultVal.IsValid)
            {
                throw new GraphQLException(ValidationError.Create(resultVal));
            }
            else
            {
                var result = await _service.ChangeStatus(dto);
                if (result != null) return result;
            }
            return null;
        }
    }
}
