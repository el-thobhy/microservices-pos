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
        public async Task<CategoryDto> AddAsync(CategoryDto dto)
        {
            var result = await _service.Add(dto);
            return result;
        }
        public async Task<CategoryDto> Update(CategoryDto dto)
        {
            try
            {
                var result = await _service.Updates(dto);
                if (result) return dto;
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync($"Error: {e.Message}");
                throw;
            }
            return null;
        }
        public async Task<CategoryStatusDto> ChangeStatus(CategoryStatusDto dto)
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
