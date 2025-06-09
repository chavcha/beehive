namespace Beehive.Models.PageModels
{
    public class ChatMessagePageModel(User me, Chat chat, ChatMessageLoader loader, OldChatMessageLoader archiveLoader)
    {
        public User Current => me;

        public Chat Chat => chat;

        public string MessageText { get; set; } = string.Empty;

        public ChatMessageLoader Loader => loader;

        public OldChatMessageLoader ArchiveLoader => archiveLoader;

        public List<Message> Messages => [.. EnumerationModel];

        public readonly IEnumerable<Message> EnumerationModel = loader.Concat(archiveLoader);
    }
}
