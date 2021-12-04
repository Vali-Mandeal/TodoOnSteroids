namespace Todo.Api.Helpers;

using Todo.Api.Entities;
using Todo.Api.Dtos;
using AutoMapper;

public class MapperProfiles : Profile
{
    public MapperProfiles()
    {
        CreateMap<TodoItem, TodoItemForReadDto>();
        CreateMap<Priority, PriorityForReadDto>();

        CreateMap<TodoItemForCreateDto, TodoItem>();

        CreateMap<TodoItemForUpdateDto, TodoItem>()
            .ForMember(todo => todo.Priority, opts => opts.Ignore());    
    }
}

