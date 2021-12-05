namespace Domain.Common.ServiceBusDtos;

public class TodoItemBase
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }
    public Guid PriorityId { get; set; }
}
