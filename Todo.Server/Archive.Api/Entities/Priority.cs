
namespace Archive.Api.Entities;
using System.ComponentModel.DataAnnotations;

public class Priority
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }
}
