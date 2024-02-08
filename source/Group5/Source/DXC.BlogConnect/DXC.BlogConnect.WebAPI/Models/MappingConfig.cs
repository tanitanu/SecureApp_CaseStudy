using AutoMapper;
using DXC.BlogConnect.WebAPI.Models.Domain;
using DXC.BlogConnect.DTO;
namespace DXC.BlogConnect.WebAPI.Models
{
    /*
* Created By: Kishore
*/
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<User, UserGetDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Role, UserRoleDTO>().ReverseMap();
        }
    }
}
