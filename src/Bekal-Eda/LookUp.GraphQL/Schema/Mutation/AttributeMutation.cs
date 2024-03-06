using LookUp.Domain.Dtos;
using LookUp.Domain.Services;

namespace LookUp.GraphQL.Schema.Mutation
{
    [ExtendObjectType(typeof(Mutation))]
    public class AttributeMutation
    {
        private readonly IAttributeService _service;
        public AttributeMutation(IAttributeService service)
        {
            _service = service;
        }

        public async Task<AttributeDto> AddAttributeAsync(AttributeDto dto)
        {
            var result = await _service.AddAttribute(dto);
            return result;
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
        public async Task<AttributeStatusDto> ChangeStatus(AttributeStatusDto dto)
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
