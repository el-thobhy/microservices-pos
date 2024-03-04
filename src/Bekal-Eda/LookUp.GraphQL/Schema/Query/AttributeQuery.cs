using LookUp.Domain.Dtos;
using LookUp.Domain.Services;

namespace LookUp.GraphQL.Schema.Query
{
    [ExtendObjectType(typeof(Query))]
    public class AttributeQuery
    {
        private readonly ILookUpService _service;
        public AttributeQuery(ILookUpService service)
        {
            _service = service;
        }
        [UsePaging]
        public async Task<IEnumerable<AttributesDto>> GetAll()
        {
            return await _service.GetAllAttributes();
        }
    }
}
