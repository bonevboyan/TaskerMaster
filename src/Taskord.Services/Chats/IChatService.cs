namespace Taskord.Services.Chats
{
    using Taskord.Services.Chats.Models;

    public interface IChatService
    {
        ChatServiceModel GetChat(string chatId);

        IEnumerable<ChatListServiceModel> GetChatNames(string teamId);
    }
}
