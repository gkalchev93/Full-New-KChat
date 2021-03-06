﻿using KChatClient.Enums;
using KChatClient.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KChatClient.Services
{
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


}
