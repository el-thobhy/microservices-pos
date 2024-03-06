using AutoMapper;
using Store.Domain.Dtos;
using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.MapProfile
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile() : base("Entity To Dto Profile")
        {
            CreateMap<CategoryEntity, CategoryDto>();
            CreateMap<CategoryDto, CategoryEntity>();
            CreateMap<CategoryInputDto, CategoryEntity>();
            CreateMap<CategoryStatusDto, CategoryEntity>();


            CreateMap<ProductEntity, ProductDto>();
            CreateMap<ProductDto, ProductEntity>();
            CreateMap<ProductCategoryChangedDto, ProductEntity>();
            CreateMap<ProductAttributeChangedDto, ProductEntity>();
            CreateMap<ProductUpdateDto,  ProductEntity>();
            CreateMap<ProductPriceVolumeChangeDto, ProductEntity>();
            CreateMap<ProductSoldStockChangeDto, ProductEntity>();
            CreateMap<ProductStatusChangeDto, ProductEntity>();
            CreateMap<ProductInputDto, ProductEntity>();

            CreateMap<AttributesEntity, AttributeDto>();

        }
    }
}
