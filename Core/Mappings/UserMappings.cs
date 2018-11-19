using AutoMapper;
using KorepetycjeNaJuz.Core.Models;
using KorepetycjeNaJuz.Core.DTO;
using System;

namespace KorepetycjeNaJuz.Core.Mappings
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<User, UserDTO>().ReverseMap(); // Two-way map
            CreateMap<UserCreateDTO, User>()
                .ForMember(x => x.UserName, opts => opts.MapFrom(i => i.Email))
                .ForMember(x => x.SecurityStamp, opts => opts.MapFrom(x => Guid.NewGuid().ToString()));
        }
    }
}
