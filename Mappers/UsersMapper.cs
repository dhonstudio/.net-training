using AutoMapper;
using traningday2.DTO;
using traningday2.Models;

namespace traningday2.Mappers
{
    public class UsersMapper : Profile
    {
        public UsersMapper() 
        {
            CreateMap<UsersParamDTO, Users>();
            CreateMap<Users, UsersDTO>();
            CreateMap<UserRoleParamDTO, UserRoles>();
            CreateMap<UserRoles, UserRolesDTO>();
        }
    }
}
