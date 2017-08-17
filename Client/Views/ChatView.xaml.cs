using System.Windows;
using System.Windows.Controls;
namespace KChatClient.Views
{
    /// <summary>
    /// Interaction logic for ChatView.xaml
    /// </summary>
    public partial class ChatView : UserControl, KChatServer.WCF.IKClient
    {
        public ChatView()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (TaskInputBox.Visibility == Visibility.Visible)
                TaskInputBox.Visibility = Visibility.Collapsed;

            else
                TaskInputBox.Visibility = Visibility.Visible;
        }

        private void SendFile(object sender, RoutedEventArgs e)
        {

        }

        public void RecieveFile(string sender, byte[] file, string fileName)
        {
            System.Windows.MessageBox.Show("You are goind to receive file !");
        }
    }
}
