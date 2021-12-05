namespace Todo.Infrastructure.Helpers;

using Todo.Infrastructure.Persistence;
using Newtonsoft.Json;
using Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;

public class DbInitializer
{
    private readonly DataContext _context;

    public DbInitializer(DataContext context)
    {
        _context = context;
    }

    public async Task MigrateAndSeed()
    {
        await _context.Database.MigrateAsync();

        if (_context.TodoItems.Any())
            return;

        var priorityData = File.ReadAllText("../Todo.Infrastructure/Helpers/DataForSeed/Priorities.json");
        var priorities = JsonConvert.DeserializeObject<List<Priority>>(priorityData);

        foreach (var priority in priorities)
            _context.Add(priority);

        _context.SaveChanges();

        var todoItemData = File.ReadAllText("../Todo.Infrastructure/Helpers/DataForSeed/Todos.json");
        var dynamicTodoItems = JsonConvert.DeserializeObject<List<dynamic>>(todoItemData);

        var todoItems = new List<TodoItem>();
        foreach (var dynamicTodoItem in dynamicTodoItems)
        {
            var todoItem = new TodoItem();


            string priorityName = dynamicTodoItem.PriorityName;

            todoItem.PriorityId = priorities.FirstOrDefault(x => x.Name == priorityName).Id;

            todoItem.IsDone = dynamicTodoItem.IsDone;   
            todoItem.Description = dynamicTodoItem.Description; 

            _context.Add(todoItem);
        }

        _context.SaveChanges();
    }   
}
        