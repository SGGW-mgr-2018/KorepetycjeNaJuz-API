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

            CreateMap<UserEditDTO, User>()
                .ForMember(x => x.FirstName, opts => opts.Condition(src => src.FirstName != null))
                .ForMember(x => x.LastName, opts => opts.Condition(src => src.LastName != null))
                .ForMember(x => x.Telephone, opts => opts.Condition(src => src.Telephone != null))
                .ForMember(x => x.Description, opts => opts.Condition(src => src.Description != null))
                .ForMember(x => x.Avatar, opts => opts.Condition(src => src.Avatar != null))
                .ForMember(x => x.PrivacyPolicesConfirmed, opts => opts.MapFrom(x => x.PrivacyPolicesConfirmed))
                .ForAllOtherMembers(x => x.Ignore());

        }
    }
}
