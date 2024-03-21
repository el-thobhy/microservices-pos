using AutoMapper;
using Product.Domain.Dto;
using Product.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.Services
{
    public interface IProductQueryService
    {
        Task<IEnumerable<ProductDto>> GetAllProduct();
        Task<ProductDto> GetProductById(Guid id);
    }
    public class ProductQueryService : IProductQueryService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        public ProductQueryService(IProductRepository productRepository, IMapper mapper)
        {
            _repository = productRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductDto>> GetAllProduct()
        {
            return _mapper.Map<IEnumerable<ProductDto>>(await _repository.GetAll());
        }

        public async Task<ProductDto> GetProductById(Guid id)
        {
            var result = _mapper.Map<ProductDto>(await _repository.GetById(id));
            return result;
        }
    }

}
