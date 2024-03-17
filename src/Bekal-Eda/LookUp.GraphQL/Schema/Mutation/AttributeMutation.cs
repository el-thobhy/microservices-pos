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
        public AttributeMutation(IAttributeService service, IValidator<AttributeDto> validator)
        {
            _service = service;
            _validator = validator;
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
        public async Task<AttributeExceptStatusDto> Update(AttributeExceptStatusDto dto)
        {
            try
            {
                var result = await _service.UpdateAttributes(dto);
                if (result) return dto;
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync($"Error: {e.Message}");
                throw;
            }
            return null;
        }
        public async Task<AttributeStatusDto> ChangeAttributeStatus(AttributeStatusDto dto)
        {
            try
            {
                var result = await _service.ChangeStatus(dto);
                if (result) return dto;
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync($"Error: {e.Message}");
                throw;
            }
            return null;
        }
    }
}
