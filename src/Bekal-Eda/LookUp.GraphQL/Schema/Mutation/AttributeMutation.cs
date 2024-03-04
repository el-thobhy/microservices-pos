using LookUp.Domain.Dtos;
using LookUp.Domain.Services;

namespace LookUp.GraphQL.Schema.Mutation
{
    [ExtendObjectType(typeof(Mutation))]
    public class AttributeMutation
    {
        private readonly ILookUpService _service;
        public AttributeMutation(ILookUpService service)
        {
            _service = service;
        }

        public async Task<AttributesDto> AddAttributeAsync(AttributesDto dto)
        {
            var result = await _service.AddAttributes(dto);
            return result;
        }
    }
}
