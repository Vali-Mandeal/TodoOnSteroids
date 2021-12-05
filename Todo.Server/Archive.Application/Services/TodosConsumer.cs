namespace Archive.Application.Services;

using MassTransit;
using System.Threading.Tasks;
using Domain.Common.Entities;
using Domain.Common.ServiceBusDtos;
using Microsoft.Extensions.Logging;
using Archive.Application.Contracts;
using AutoMapper;

public class TodosConsumer : IConsumer<TodoItemForArchiving>
{
    private readonly ILogger<TodosConsumer> _logger;
    private readonly IMapper _mapper;
    private readonly ITodosService _todosService;

    public TodosConsumer(ILogger<TodosConsumer> logger, IMapper mapper, ITodosService todosService)
    {
        _logger = logger;
        _mapper = mapper;
        _todosService = todosService;
    }
    public async Task Consume(ConsumeContext<TodoItemForArchiving> context)
    {
        var todoItemForArchiving = context.Message; 

        var todoItem = _mapper.Map<TodoItem>(todoItemForArchiving);

        _logger.LogInformation($"Received from message queue todo with id: {todoItem.Id}. Saving in database.");

        await _todosService.Create(todoItem);
    }
}

    