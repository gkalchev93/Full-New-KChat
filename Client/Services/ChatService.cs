using KChatClient.Enums;
using KChatClient.Models;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace KChatClient.Services
{
    public class ChatService : IChatService
    {
        public event Action<string, string, MessageType> NewMessage;
        public event Action<string, byte[]> NewFileSend;
        public event Action<string, string, string> SetNewTask;
        public event Action<string> ShowUserTasks;
        public event Action<string> ParticipantDisconnected;
        public event Action<User> ParticipantLoggedIn;
        public event Action<string> ParticipantLoggedOut;
        public event Action<string> ParticipantReconnected;
        public event Action ConnectionReconnecting;
        public event Action ConnectionReconnected;
        public event Action ConnectionClosed;

        private IHubProxy hubProxy;
        private HubConnection connection;
        private string url = "http://192.168.0.102:8080/kchat";

        public async Task ConnectAsync()
        {
            connection = new HubConnection(url);
            hubProxy = connection.CreateHubProxy("ChatHub");
            hubProxy.On<User>("ParticipantLogin", (u) => ParticipantLoggedIn?.Invoke(u));
            hubProxy.On<string>("ParticipantLogout", (n) => ParticipantLoggedOut?.Invoke(n));
            hubProxy.On<string>("ParticipantDisconnection", (n) => ParticipantDisconnected?.Invoke(n));
            hubProxy.On<string>("ParticipantReconnection", (n) => ParticipantReconnected?.Invoke(n));
            hubProxy.On<string, string>("BroadcastMessage", (n, m) => NewMessage?.Invoke(n, m, MessageType.Broadcast));
            hubProxy.On<string, string>("UnicastMessage", (n, m) => NewMessage?.Invoke(n, m, MessageType.Unicast));
            hubProxy.On<string, byte[]>("SendFile", (n, m) => NewFileSend?.Invoke(n, m));
            hubProxy.On<string, string, string>("SetNewTask", (n, m, k) => SetNewTask?.Invoke(n, m, k));
            hubProxy.On<string>("ShowUserTasks", (n) => ShowUserTasks?.Invoke(n));
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

        public async Task ShowUserTasksAsync(string name)
        {
            await hubProxy.Invoke("ShowUserTasks", new object[] { name });
        }
    }
}
