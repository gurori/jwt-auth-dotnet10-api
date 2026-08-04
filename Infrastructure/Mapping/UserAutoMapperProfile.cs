using Application.Models.Users;
using AutoMapper;
using Core.Entities;

namespace Infrastructure.Mapping
{
    public class UserAutoMapperProfile : Profile
    {
        public UserAutoMapperProfile()
        {
            CreateMap<UserEntity, UserResponse>();
        }
    }
}
