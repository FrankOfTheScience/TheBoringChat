namespace TheBoringChat.WebApi.Hubs
{
    public sealed class BoringChatHub : Hub
    {
        private readonly SharedDb _sharedDb;
        public BoringChatHub(SharedDb sharedDb)
            => _sharedDb = sharedDb;
        public async Task JoinGeneralChatRoom(UserConnection conn)
        {
            await Clients.All.SendAsync("JoinGeneralChatRoom", "admin", $"*** '{conn.Username}' has joined the chat");
        }

        public async Task JoinSpecificChatRoom(UserConnection conn)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, conn.ChatRoom);
            _sharedDb.connections[Context.ConnectionId] = conn;
            await Clients.Group(conn.ChatRoom).SendAsync("JoinSpecificChatRoom", "MASTER OF BOREDOM", $"'{conn.Username}' has joined the '{conn.ChatRoom}' chat");
        }

        public async Task SendMessage(string msg)
        {
            if (_sharedDb.connections.TryGetValue(Context.ConnectionId, out UserConnection conn))
                await Clients.Group(conn.ChatRoom).SendAsync("ReceiveSpecificChatMessage", conn.Username, msg);
        }
    }
}
