using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;

namespace TestClient
{
	public class User
	{
		public string Name { get; set; }
		public string ID { get; set; }
		public byte[] Photo { get; set; }
	}

	public enum MessageType
	{
		Broadcast,
		Unicast
	}

	public interface IChatService
	{
		event Action<User> ParticipantLoggedIn;
		event Action<string> ParticipantLoggedOut;
		event Action<string> ParticipantDisconnected;
		event Action<string> ParticipantReconnected;
		event Action ConnectionReconnecting;
		event Action ConnectionReconnected;
		event Action ConnectionClosed;
		event Action<string, string, MessageType> NewMessage;
		event Action<string, string, string> SetNewTask;

		Task ConnectAsync();
		Task<List<User>> LoginAsync(string name, byte[] photo);
		Task LogoutAsync();
		Task SendBroadcastMessageAsync(string msg);
		Task SendUnicastMessageAsync(string recepient, string msg);
		Task SetNewTaskAsync(string recepient, string taskDesc, string taskPriority);
	}


	public class ChatService : IChatService
	{
		public event Action<string, string, MessageType> NewMessage;
		public event Action<string, string, string> SetNewTask;
		public event Action<string> ParticipantDisconnected;
		public event Action<User> ParticipantLoggedIn;
		public event Action<string> ParticipantLoggedOut;
		public event Action<string> ParticipantReconnected;
		public event Action ConnectionReconnecting;
		public event Action ConnectionReconnected;
		public event Action ConnectionClosed;

		private IHubProxy hubProxy;
		private HubConnection connection;
		private string url = "http://{0}:{1}/kchat";

		public async Task ConnectAsync()
		{

			connection = new HubConnection(string.Format(url, "127.0.0.1", "8080"));
			hubProxy = connection.CreateHubProxy("ChatHub");
			hubProxy.On<User>("ParticipantLogin", (u) => ParticipantLoggedIn?.Invoke(u));
			hubProxy.On<string>("ParticipantLogout", (n) => ParticipantLoggedOut?.Invoke(n));
			hubProxy.On<string>("ParticipantDisconnection", (n) => ParticipantDisconnected?.Invoke(n));
			hubProxy.On<string>("ParticipantReconnection", (n) => ParticipantReconnected?.Invoke(n));
			hubProxy.On<string, string>("BroadcastMessage", (n, m) => NewMessage?.Invoke(n, m, MessageType.Broadcast));
			hubProxy.On<string, string>("UnicastMessage", (n, m) => NewMessage?.Invoke(n, m, MessageType.Unicast));
			hubProxy.On<string, string, string>("SetNewTask", (n, m, k) => SetNewTask?.Invoke(n, m, k));
			connection.Reconnecting += Reconnecting;
			connection.Reconnected += Reconnected;
			connection.Closed += Disconnected;

			ServicePointManager.DefaultConnectionLimit = 10;
			await connection.Start();
		}

		private void Disconnected()
		{
			ConnectionClosed?.Invoke();
		}

		private void Reconnected()
		{
			ConnectionReconnected?.Invoke();
		}

		private void Reconnecting()
		{
			ConnectionReconnecting?.Invoke();
		}

		public async Task<List<User>> LoginAsync(string name, byte[] photo)
		{
			return await hubProxy.Invoke<List<User>>("Login", new object[] { name, photo });
		}

		public async Task LogoutAsync()
		{
			await hubProxy.Invoke("Logout");
		}

		public async Task SendBroadcastMessageAsync(string msg)
		{
			await hubProxy.Invoke("BroadcastChat", msg);
		}

		public async Task SendUnicastMessageAsync(string recepient, string msg)
		{
			await hubProxy.Invoke("UnicastChat", new object[] { recepient, msg });
		}

		public async Task SendFileAsync(string recepient, byte[] file)
		{
			await hubProxy.Invoke("SendFile", new object[] { recepient, file });
		}

		public async Task SetNewTaskAsync(string recepient, string taskD, string taskP)
		{
			await hubProxy.Invoke("SetNewTask", new object[] { recepient, taskD, taskP });
		}
	}
}

