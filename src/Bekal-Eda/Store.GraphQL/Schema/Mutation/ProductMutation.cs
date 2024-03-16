using FluentValidation;
using FluentValidation.Results;
using Framework.Validation;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Store.Domain.Dtos;
using Store.Domain.Services;

namespace Store.GraphQL.Schema.Mutation
{
    [ExtendObjectType(typeof(Mutation))]
    public class ProductMutation
    {
        private readonly IProductService _service;
        private IValidator<ProductCreateDto> _validator;
        public ProductMutation(IProductService service, IValidator<ProductCreateDto> validator)
        {
            _service = service;
            _validator = validator;
        }
        public async Task<ProductDto> AddProductAsync(ProductCreateDto dto)
        {
            ValidationResult resultVal = await _validator.ValidateAsync(dto);
            if (!resultVal.IsValid)
            {
                throw new GraphQLException(ValidationError.Create(resultVal));
            }
            else
            {
                var result = await _service.AddProduct(dto);
                return result;
            }

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
