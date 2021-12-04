namespace Todo.Api.Dtos;

public class TodoItemForReadDto
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }

    public PriorityForReadDto Priority { get; set; }
}

