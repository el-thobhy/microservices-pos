using Store.Domain.Dtos;
using Store.Domain.Services;

namespace Store.GraphQL.Schema.Query
{
    [ExtendObjectType(typeof(Query))]
    public class CategoryQuery
    {
        private readonly ICategoryService _service;
        public CategoryQuery(ICategoryService service)
        {
            _service = service;
        }
        [UsePaging]
        public async Task<IEnumerable<CategoryDto>> GetAll()
        {
            return await _service.GetAlls();
        }

        public async Task<CategoryDto> GetById(Guid id)
        {
            return await _service.GetById(id);
        }

    }
}
