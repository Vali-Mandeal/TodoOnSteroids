namespace Archive.Api.Consumers;

using MassTransit;
using System.Threading.Tasks;
using Domain.Common.Entities;

public class TodosConsumer : IConsumer<TodoItem>
{
    private readonly ILogger<TodosConsumer> _logger;
    public TodosConsumer(ILogger<TodosConsumer> logger)
    {
        _logger = logger;
    }
    public async Task Consume(ConsumeContext<TodoItem> context)
    {
        var data = context.Message;

        _logger.LogInformation(data.Description);
    }
}

