using Store.Domain.Dtos;
using Store.Domain.Services;

namespace Store.GraphQL.Schema.Mutation
{
    [ExtendObjectType(typeof(Mutation))]
    public class CategoryMutation
    {
        private readonly ICategoryService _service;
        public CategoryMutation(ICategoryService service)
        {
            _service = service;
        }
        public async Task<CategoryDto> AddAsync(CategoryInputDto dto)
        {
            var result = await _service.Add(dto);
            return result;
        }
        public async Task<CategoryDto> Update(CategoryInputDto dto)
        {
            try
            {
                var result = await _service.Updates(dto);
                if (result != null) return result;
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync($"Error: {e.Message}");
                throw;
            }
            return null;
        }
        public async Task<CategoryDto> ChangeStatus(CategoryStatusDto dto)
        {
            try
            {
                var result = await _service.ChangeStatus(dto);
                if (result != null) return result;
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
