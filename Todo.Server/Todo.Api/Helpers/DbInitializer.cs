namespace Todo.Api.Helpers;

using Todo.Api.Persistence;
//using System.Text.Json;
using Newtonsoft.Json;
using Todo.Api.Entities;

public class DbInitializer
{
    public static void SeedData(DataContext context)
    {
        if (context.TodoItems.Any())
            return;

        var priorityData = File.ReadAllText("Helpers/DataForSeed/Priorities.json");
        var priorities = JsonConvert.DeserializeObject<List<Priority>>(priorityData);

        foreach (var priority in priorities)
            context.Add(priority);

        context.SaveChanges();

        var todoItemData = File.ReadAllText("Helpers/DataForSeed/Todos.json");
        var dynamicTodoItems = JsonConvert.DeserializeObject<List<dynamic>>(todoItemData);

        var todoItems = new List<TodoItem>();
        foreach (var dynamicTodoItem in dynamicTodoItems)
        {
            var todoItem = new TodoItem();


            string priorityName = dynamicTodoItem.PriorityName;

            todoItem.PriorityId = priorities.FirstOrDefault(x => x.Name == priorityName).Id;

            todoItem.IsDone = dynamicTodoItem.IsDone;   
            todoItem.Description = dynamicTodoItem.Description; 

            context.Add(todoItem);
        }

        context.SaveChanges();
    }   
}
        