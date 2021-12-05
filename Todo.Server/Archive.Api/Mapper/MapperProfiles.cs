namespace Archive.Api.Mapper;

using AutoMapper;
using Domain.Common.Entities;
using Domain.Common.ServiceBusDtos;

public class MapperProfiles : Profile
{
    public MapperProfiles()
    {
        CreateMap<TodoItem, TodoItemForUnarchiving>();
        CreateMap<TodoItemForArchiving, TodoItem>();
    }
}
