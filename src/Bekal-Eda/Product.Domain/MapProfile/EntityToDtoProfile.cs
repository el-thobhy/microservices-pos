using AutoMapper;
using Product.Domain.Dto;
using Product.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.MapProfile
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile() : base("Entity To Dto Profile")
        {
            CreateMap<ProductEntity, ProductDto>();
            CreateMap<ProductDto, ProductEntity>();
        }
    }
}
