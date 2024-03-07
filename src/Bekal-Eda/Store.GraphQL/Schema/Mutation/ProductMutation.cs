using Store.Domain.Dtos;
using Store.Domain.Services;

namespace Store.GraphQL.Schema.Mutation
{
    [ExtendObjectType(typeof(Mutation))]
    public class ProductMutation
    {
        private readonly IProductService _service;
        public ProductMutation(IProductService service)
        {
            _service = service;
        }
        public async Task<ProductDto> AddProductAsync(ProductCreateDto dto)
        {
            var result = await _service.AddProduct(dto);
            return result;
        }
        public async Task<ProductDto> UpdateProduct(ProductUpdateDto dto)
        {
            try
            {
                var result = await _service.UpdateProduct(dto);
                if (result != null) return result;
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync($"Error: {e.Message}");
                throw;
            }
            return null;
        }
        public async Task<ProductDto> ChangeProductStatus(ProductStatusChangeDto dto)
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
        public async Task<ProductDto> ChangeCategory(ProductCategoryChangedDto dto)
        {
            try
            {
                var result = await _service.ChangeCategory(dto);
                if (result != null) return result;
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync($"Error: {e.Message}");
                throw;
            }
            return null;
        }
        public async Task<ProductDto> ChangeAttribute(ProductAttributeChangedDto dto)
        {
            try
            {
                var result = await _service.ChangeAttribute(dto);
                if (result != null) return result;
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync($"Error: {e.Message}");
                throw;
            }
            return null;
        }
        public async Task<ProductDto> ChangePriceVolume(ProductPriceVolumeChangeDto dto)
        {
            try
            {
                var result = await _service.ChangePriceVolume(dto);
                if (result != null) return result;
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync($"Error: {e.Message}");
                throw;
            }
            return null;
        }
        public async Task<ProductDto> ChangeStockSold(ProductSoldStockChangeDto dto)
        {
            try
            {
                var result = await _service.ChangeStockSold(dto);
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
