using LookUp.Domain.Dtos;
using LookUp.Domain.Services;

namespace LookUp.GraphQL.Schema.Query
{
    [ExtendObjectType(typeof(Query))]
    public class AttributeQuery
    {
        private readonly IAttributeService _service;
        public AttributeQuery(IAttributeService service)
        {
            _service = service;
        }
        [UsePaging]
        public async Task<IEnumerable<AttributeDto>> GetAllAttribute()
        {
            return await _service.GetAllAttributes();
        }

        public async Task<AttributeDto> GetAttributeById(Guid id)
        {
            return await _service.GetAttributeById(id);
        }

    }
}
