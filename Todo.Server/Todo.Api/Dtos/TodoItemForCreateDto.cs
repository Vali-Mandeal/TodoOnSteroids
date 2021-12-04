namespace Todo.Api.Dtos;

public class TodoItemForCreateDto
{
    public string Description { get; set; }

    public Guid PriorityId { get; set; }
}

