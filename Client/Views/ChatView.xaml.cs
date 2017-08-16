using KChat.KChatWcfService;
using System.IO;
using System.Windows;
using System.Windows.Controls;
namespace KChatClient.Views
{
    /// <summary>
    /// Interaction logic for ChatView.xaml
    /// </summary>
    public partial class ChatView : UserControl
    {
        public ChatView()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (TaskInputBox.Visibility == Visibility.Visible)
            {
                TaskInputBox.Visibility = Visibility.Collapsed;
            }
            else
                TaskInputBox.Visibility = Visibility.Visible;
        }

        private void SendFile(object sender, RoutedEventArgs e)
        {
            Services.DialogService dialogService = new Services.DialogService();

            var filePath = dialogService.OpenFile("Select file to send", "");

            if (!string.IsNullOrEmpty(filePath))
            {
                IKWcfService clientUpload = new KChat.KChatWcfService.KWcfServiceClient();
                clientUpload.UploadFile(File.ReadAllBytes(filePath), Path.GetFileName(filePath));
            }
        }


    }
}
