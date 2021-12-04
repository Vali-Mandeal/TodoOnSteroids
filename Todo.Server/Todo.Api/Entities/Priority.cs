using System.ComponentModel.DataAnnotations;

namespace Todo.Api.Entities;
public class Priority
{
    public Priority()   
    {
        Id = Guid.NewGuid();
    }

    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }
}
        