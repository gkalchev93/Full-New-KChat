using KChat.KChatWcfService;
using System.Windows;

namespace KChat.Views
{
	/// <summary>
	/// Interaction logic for TaskList.xaml
	/// </summary>
	public partial class TaskList : Window
	{
		IKWcfService wcfClient;
		string userName;
		public TaskList(string user, IKWcfService client)
		{
			InitializeComponent();
			wcfClient = client;
			userName = user;
			LoadData();
		}

		private void LoadData()
		{
			var userTasksTable = wcfClient.GetTasks(userName);
			dgTasks.Items.Clear();
			dgTasks.ItemsSource = userTasksTable.DefaultView;
		}
	}
}
