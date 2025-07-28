using Microsoft.AspNetCore.SignalR;

namespace RealtimeChatApp.Hubs;

public class ChatHub : Hub
{
    public async Task SendMessage(string user, string message) =>
        Clients.All.SendAsync("ReceiveMessage", user, message);

    public override async Task OnConnectedAsync()
    {
        await Clients.All.SendAsync("ReceiveMessage", "System", $"{Context.ConnectionId} has joined the chat.");
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await Clients.All.SendAsync("ReceiveMessage", "System", $"{Context.ConnectionId} has left the chat.");
        await base.OnDisconnectedAsync(exception);
    }
}