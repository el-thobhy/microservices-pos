using HotChocolate.Authorization;
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
        [Authorize(Roles = new[] { "administrator", "customer" })]
        public async Task<IEnumerable<CategoryDto>> GetAllCategory()
        {
            return await _service.GetAlls();
        }

        [Authorize(Roles = new[] { "administrator" })]
        public async Task<CategoryDto> GetCategoryById(Guid id)
        {
            return await _service.GetById(id);
        }

    }
}
