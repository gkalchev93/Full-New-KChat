using KChat.KChatWcfService;
using System.IO;

namespace KChatClient.Services
{
    public static class KWcfService
    {
        static IKWcfService wcfClient;

        public static void Login(string userName)
        {
            wcfClient = new KChat.KChatWcfService.KWcfServiceClient();
            wcfClient.Login(userName);
        }

        public static void SendFile(string sender)
        {
            DialogService dialogService = new DialogService();

            var filePath = dialogService.OpenFile("Select file to send", "");

            if (!string.IsNullOrEmpty(filePath))
            {
                wcfClient.SendFile(sender, File.ReadAllBytes(filePath), Path.GetFileName(filePath));
            }
        }
    }
}
