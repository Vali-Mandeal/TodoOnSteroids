namespace Todo.Api.Dtos;

public class TodoItemForUpdateDto
{
    public string Description { get; set; }
    public bool IsDone { get; set; }
    public Guid PriorityId { get; set; }
}
