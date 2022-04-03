namespace Taskord.Services.Chats
{
    using Taskord.Services.Chats.Models;

    public interface IChatService
    {
        IEnumerable<ChatListServiceModel> GetChatList(string teamId, string userId, string selectedChatId = null);

        string CreateTeamChat(string teamId, string name);

        string CreatePersonalChat(string firstUserId, string secondUserId);

        ChatServiceModel GetPersonalChat(string firstUserId, string secondUserId);

        ChatServiceModel GetTeamChat(string userId, string teamId, string chatId);

        string SendMessage(string chatId, string userId, string content);

        ChatMessageServiceModel GetLastMessage(string userId, string chatId);

        bool IsChatRead(string userId, string chatId);

        bool IsUserInChat(string userId, string chatId);
    }
}
