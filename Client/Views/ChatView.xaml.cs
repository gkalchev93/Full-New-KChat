using KChat.KChatWcfService;
using System.IO;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
namespace KChatClient.Views
{
	/// <summary>
	/// Interaction logic for ChatView.xaml
	/// </summary>
	public partial class ChatView : UserControl, IKWcfServiceCallback
	{
		IKWcfService wcfClient;
		Services.DialogService dialogService;
		public ChatView()
		{
			InitializeComponent();
			dialogService = new Services.DialogService();
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
			var senderObj = sender as Button;
			var receiver = senderObj.Tag.ToString();

			var filePath = dialogService.OpenFile("Select file to send:");

			wcfClient.SendFile(receiver, File.ReadAllBytes(filePath), Path.GetFileName(filePath));
		}

		public void RecieveFile(string sender, byte[] file, string fileName)
		{
			var confirmation = dialogService.ShowConfirmationRequest($"You are goind to receive file from {sender}. Do you want to receive it?", "KChat");
			if (confirmation)
			{
				dialogService.SaveFile("Select folder to save the file", fileName, file);
			}
		}
		public void Login(object sender, RoutedEventArgs e)
		{
			InstanceContext context = new InstanceContext(this);
			wcfClient = new KChat.KChatWcfService.KWcfServiceClient(context);
			wcfClient.Login(tbUserName.Text);
		}

		public void ShowTasks(object sender, RoutedEventArgs e)
		{
			KChat.Views.TaskList tskDlg = new KChat.Views.TaskList(tbUserName.Text, wcfClient);
			tskDlg.ShowDialog();
		}

		private void Logout(object sender, RoutedEventArgs e)
		{
			if (wcfClient != null)
				wcfClient.Logout();
		}
	}
}
