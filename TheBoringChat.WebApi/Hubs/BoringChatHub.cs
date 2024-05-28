using TheBoringChat.WebApi.Models;

namespace TheBoringChat.WebApi.Hubs
{
    public sealed class BoringChatHub : Hub
    {
        public async Task JoinGeneralChatRoom(UserConnection conn)
        {
            await Clients.All.SendAsync("ReceiveMessage", "admin", $"{conn.Username} has joined the chat");
        }

        public async Task JoinSpecificChatRoom(UserConnection conn)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, conn.ChatRoom);
            await Clients.Group(conn.ChatRoom).SendAsync("ReceiveMessage", "admin", $"{conn.Username} has joined the {conn.ChatRoom} chat");
        }
    }
}
