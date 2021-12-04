namespace Todo.Api.Dtos;

public class TodoItemForUpdateDto
{
    public string Description { get; set; }

    public Guid PriorityId { get; set; }
}
