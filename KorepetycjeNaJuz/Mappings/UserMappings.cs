using AutoMapper;
using KorepetycjeNaJuz.Core.Models;
using KorepetycjeNaJuz.Data.DTO;

namespace KorepetycjeNaJuz.Mappings
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<User, UserDTO>().ReverseMap(); // Two-way map
        }
    }
}
