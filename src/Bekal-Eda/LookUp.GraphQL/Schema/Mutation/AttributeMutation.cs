using FluentValidation;
using FluentValidation.Results;
using Framework.Validation;
using LookUp.Domain.Dtos;
using LookUp.Domain.Services;

namespace LookUp.GraphQL.Schema.Mutation
{
    [ExtendObjectType(typeof(Mutation))]
    public class AttributeMutation
    {
        private readonly IAttributeService _service;
        private IValidator<AttributeDto> _validator;
        private IValidator<AttributeExceptStatusDto> _validatorUpdate;
        private IValidator<AttributeStatusDto> _validatorChangeStatus;
        public AttributeMutation(IAttributeService service, IValidator<AttributeDto> validator, IValidator<AttributeExceptStatusDto> validatorUpdate, IValidator<AttributeStatusDto> validatorChangeStatus)
        {
            _service = service;
            _validator = validator;
            _validatorUpdate = validatorUpdate;
            _validatorChangeStatus = validatorChangeStatus;

        }

        public async Task<AttributeDto> AddAttributeAsync(AttributeDto dto)
        {
            ValidationResult resultVal = await _validator.ValidateAsync(dto);
            if(!resultVal.IsValid)
            {
                throw new GraphQLException(ValidationError.Create(resultVal));
            }
            else
            {
                var result = await _service.AddAttribute(dto);
                return result;
            }
        }
        public async Task<AttributeExceptStatusDto> UpdateAttribute(AttributeExceptStatusDto dto)
        {
            ValidationResult resultVal = await _validatorUpdate.ValidateAsync(dto);
            if (!resultVal.IsValid)
            {
                throw new GraphQLException(ValidationError.Create(resultVal));
            }
            else 
            {
                var result = await _service.UpdateAttributes(dto);
                if (result) return dto;
            }
            return null;
        }
        public async Task<AttributeStatusDto> ChangeAttributeStatus(AttributeStatusDto dto)
        {
            ValidationResult resultVal = await _validatorChangeStatus.ValidateAsync(dto);
            if(!resultVal.IsValid)
            {
                throw new GraphQLException(ValidationError.Create(resultVal));
            }
            else
            {
                var result = await _service.ChangeStatus(dto);
                if (result) return dto;
            }
            return null;
        }
    }
}
