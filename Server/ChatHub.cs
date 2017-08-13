using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Microsoft.AspNet.SignalR;

namespace KChatServer
{
    public class ChatHub : Hub<IClient>
    {
        private static ConcurrentDictionary<string, User> ChatClients = new ConcurrentDictionary<string, User>();

        public override Task OnDisconnected(bool stopCalled)
        {
            var userName = ChatClients.SingleOrDefault((c) => c.Value.ID == Context.ConnectionId).Key;
            if (userName != null)
            {
                Clients.Others.ParticipantDisconnection(userName);
                Console.WriteLine($"<> {userName} is disconnected");
            }
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            var userName = ChatClients.SingleOrDefault((c) => c.Value.ID == Context.ConnectionId).Key;
            if (userName != null)
            {
                Clients.Others.ParticipantReconnection(userName);
                Console.WriteLine($"== {userName} is reconnected");
            }
            return base.OnReconnected();
        }

        public List<User> Login(string name, byte[] photo)
        {
            if (!ChatClients.ContainsKey(name))
            {
                Console.WriteLine($"++ {name} is logged in");
                List<User> users = new List<User>(ChatClients.Values);
                User newUser = new User { Name = name, ID = Context.ConnectionId, Photo = photo };
                var added = ChatClients.TryAdd(name, newUser);
                if (!added) return null;
                Clients.CallerState.UserName = name;
                Clients.Others.ParticipantLogin(newUser);
                return users;
            }
            return null;
        }

        public void Logout()
        {
            var name = Clients.CallerState.UserName;
            if (!string.IsNullOrEmpty(name))
            {
                User client = new User();
                ChatClients.TryRemove(name, out client);
                Clients.Others.ParticipantLogout(name);
                Console.WriteLine($"-- {name} has logged out");
            }
        }

        public void BroadcastChat(string message)
        {
            var name = Clients.CallerState.UserName;
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(message))
            {
                Clients.Others.BroadcastMessage(name, message);
            }
        }

        public void UnicastChat(string recepient, string message)
        {
            var sender = Clients.CallerState.UserName;
            if (!string.IsNullOrEmpty(sender))
            {
                if (recepient != sender)
                {
                    if (!string.IsNullOrEmpty(message))
                    {
                        if (ChatClients.ContainsKey(recepient))
                        {
                            User client = new User();
                            ChatClients.TryGetValue(recepient, out client);
                            Clients.Client(client.ID).UnicastMessage(sender, message);
                        }
                    }
                }
            }
        }

        public void SendFile(string recepient, byte[] file)
        {
            var sender = Clients.CallerState.UserName;
            if (!string.IsNullOrEmpty(sender))
            {
                if (recepient != sender)
                {
                    if (file.Length > 0)
                    {
                        if (ChatClients.ContainsKey(recepient))
                        {
                            User client = new User();
                            ChatClients.TryGetValue(recepient, out client);
                            Clients.Client(client.ID).SendFile(sender, file);
                        }
                    }
                }
            }
        }

        public void SetNewTask(string recepient, string taskDesc)
        {
            var sender = Clients.CallerState.UserName;
            if (!string.IsNullOrEmpty(sender))
            {
                if (recepient != sender)
                {
                    if (!string.IsNullOrEmpty(taskDesc))
                    {
                        if (ChatClients.ContainsKey(recepient))
                        {
                            KChatTask task = new KChatTask(sender, recepient, taskDesc);

                            User client = new User();
                            ChatClients.TryGetValue(recepient, out client);
                            Console.WriteLine($"{sender} set task to {recepient}");

                            DbHelper.SetTask(task);
                            Clients.Client(client.ID).SetTask(sender, taskDesc);
                        }
                    }
                }
            }
        }
    }
}

