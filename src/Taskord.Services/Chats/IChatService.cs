namespace Taskord.Services.Chats
{
    using Taskord.Services.Chats.Models;

    public interface IChatService
    {
        IEnumerable<ChatListServiceModel> GetChatList(string teamId);

        string CreateChat(string teamId, string name, IEnumerable<string> userIds);

        string CreatePersonalChat(string firstUserId, string secondUserId);

        ChatServiceModel GetPersonalChat(string firstUserId, string secondUserId);

        ChatServiceModel GetTeamChat(string teamId, string chatId);

        string SendMessage(string chatId, string userId, string content);

        ChatMessageServiceModel GetLastMessage(string userId, string secondUserId);
    }
}
