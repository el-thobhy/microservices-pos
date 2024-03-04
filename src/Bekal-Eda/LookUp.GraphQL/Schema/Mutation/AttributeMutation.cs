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
        public async Task<AttributeDto> Update(AttributeDto dto)
        {
            return await _service.UpdateAttributes(dto);
        }
    }
}
