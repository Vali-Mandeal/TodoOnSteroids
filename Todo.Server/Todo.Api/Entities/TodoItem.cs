﻿namespace Todo.Api.Entities;

public class TodoItem
{
    public int Id { get; set; }
    public string Description { get; set; }
    public Priority Priority { get; set; }
    public bool IsDone { get; set; }
}