using AutoMapper;
using LookUp.Domain.Dtos;
using LookUp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookUp.Domain.MapProfile
{
    public class EntityToDtoProfile: Profile
    {
        public EntityToDtoProfile(): base("Entity To Dto Profile")
        {
            CreateMap<AttributesEntity, AttributeDto>();
            CreateMap<AttributeDto, AttributesEntity>();
            CreateMap<AttributeExceptStatusDto, AttributesEntity>();
            CreateMap<AttributeStatusDto, AttributesEntity>();
        }
    }
}
