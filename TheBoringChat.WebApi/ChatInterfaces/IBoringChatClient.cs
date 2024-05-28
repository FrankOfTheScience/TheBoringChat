using TheBoringChat.WebApi.Models;

namespace TheBoringChat.WebApi.ChatInterfaces
{
    public interface IBoringChatClient
    {
        Task ReceiveMessage(string message);
        Task JoinChat(UserConnection conn);
    }
}
