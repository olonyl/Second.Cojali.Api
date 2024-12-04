
using AutoMapper;
using Second.Cojali.Api.Contracts.Dtos.Request;
using Second.Cojali.Api.Contracts.Dtos.Response;
using Second.Cojali.Application.Models;

namespace Second.Cojali.Api.IoC.Mappings;

public class ApiMappingProfile : Profile
{
    public ApiMappingProfile()
    {
        CreateMap<UserDto, UserResponseDto>();
    }
}
