namespace Archive.Api.Entities;

using System.ComponentModel.DataAnnotations;

public class TodoItem
{
    [Key]
    public Guid Id { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }

    public Guid PriorityId { get; set; }
    public Priority Priority { get; set; }
}
