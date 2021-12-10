namespace Todo.Api.Hubs;

using Microsoft.AspNetCore.SignalR;

public class TodoHub : Hub
{
    private readonly ILogger _logger;

    private static List<string> _connectedUsers = new();

    public TodoHub(ILogger<TodoHub> logger)
    {
        _logger = logger;
    }

    public override Task OnConnectedAsync()
    {
        var user = _connectedUsers.FirstOrDefault(Context.ConnectionId);

        if (user is null)
            _connectedUsers.Add(Context.ConnectionId);

        _logger.LogInformation($"User {Context.ConnectionId} connected. Active connections: {_connectedUsers.Count}");

        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        var user = _connectedUsers.FirstOrDefault(Context.ConnectionId);

        if (user is not null)
            _connectedUsers.Remove(Context.ConnectionId);

        _logger.LogInformation($"User {Context.ConnectionId} disconnected. Remaining active connections: {_connectedUsers.Count}");

        return base.OnDisconnectedAsync(exception);
    }
}
