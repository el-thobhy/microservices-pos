using AutoMapper;
using Order.Domain.Dtos;
using Order.Domain.Entities;

namespace Order.Domain.MapProfile
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile() : base("Entity To Dto Profile")
        {
            CreateMap<CartEntity, CartDto>();
            CreateMap<CartDto, CartEntity>();
            CreateMap<CartProductEntity, CartProductDto>();
            CreateMap<CartProductDto, CartProductEntity>();

            CreateMap<UserEntity, UserDto>();
            CreateMap<ProductEntity, ProductDto>();
        }
    }
}
