using AutoMapper;
using Books.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagment.API.DTOs;

namespace Books.API.Profiles
{
    public class UserManagmentProfile : Profile
    {
        public UserManagmentProfile()
        {
            CreateMap<User,UserDto>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ForMember(dto => dto.Permissions, c => c.MapFrom(c => string.Join(",",c.UserPermissions.Select(cs => cs.Permission.Code))))
            .ReverseMap();

            CreateMap<UserCreationDto, User>();

        }
    }
}
