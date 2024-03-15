using AutoMapper;
using Payment.Domain.Dtos;
using Payment.Domain.Entity;
using Payment.Domain.EventEnvelopes;

namespace Payment.Domain.MapProfile
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile() : base("Entity To Dto profile")
        {
            CreateMap<PaymentEntity, PaymentDto>();
            CreateMap<UserEntity, UserDto>();
            CreateMap<CartProductEntity, CartProductItem>();
        }
    }
}
