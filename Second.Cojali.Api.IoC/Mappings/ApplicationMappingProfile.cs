
using AutoMapper;
using Second.Cojali.Application.Models;
using Second.Cojali.Domain.Entities;

namespace Second.Cojali.Api.IoC.Mappings;

public class ApplicationMappingProfile : Profile
{
    public ApplicationMappingProfile()
    {
        CreateMap<UserDto, User>().ReverseMap();
    }
}
