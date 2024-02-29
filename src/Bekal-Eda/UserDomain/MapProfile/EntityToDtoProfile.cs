using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Dtos;
using User.Domain.Entities;

namespace User.Domain.MapProfile
{
    public class EntityToDtoProfile: Profile
    {
        public EntityToDtoProfile(): base("Entity to Dto profile")
        {
            CreateMap<UserEntity, UserDto>()
                .ForMember(trg => trg.Password, org => org.Ignore()); //trg target, origin = org, untuk menjaga agar password tidak terekspos
            //antara target dan origin yang sudah sama maka tidak perlu dilakukan mapper, tapi jika pada user entity dan user dto berbeda maka
            //perlu mapper
            //  .ForMember(trg => trg.FName, org => org.MapFrom(org=>org.FirstName)


            CreateMap<UserDto, UserEntity>();
            CreateMap<UserEntity, LoginDto>();
        }
    }
}
