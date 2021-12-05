namespace Todo.Api.Mapper;

using Domain.Common.Entities;
using Domain.Common.ServiceBusDtos;
using Todo.Api.Dtos;
using AutoMapper;

public class MapperProfiles : Profile
{
    public MapperProfiles()
    {   
        CreateMap<TodoItem, TodoItemForReadDto>();
        CreateMap<Priority, PriorityForReadDto>();

        CreateMap<TodoItemForCreateDto, TodoItem>();

        CreateMap<TodoItemForUpdateDto, TodoItem>();

        CreateMap<TodoItem, TodoItem>()
            .ForMember(todo => todo.Priority, opts => opts.Ignore())
            .ForMember(todo => todo.Id, opts => opts.Ignore());

        CreateMap<TodoItem, TodoItemForArchiving>();
        CreateMap<TodoItemForUnarchiving, TodoItem>();

    }
}

