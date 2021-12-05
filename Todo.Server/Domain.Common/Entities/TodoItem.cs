using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Domain.Common.Entities;
public class TodoItem
{
    public TodoItem()
    {
        Id = Guid.NewGuid();
    }

    [Key]
    public Guid Id { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }

    public Guid PriorityId { get; set; }
    public Priority Priority { get; set; }
}
