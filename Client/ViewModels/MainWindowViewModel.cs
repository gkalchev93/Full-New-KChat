﻿using KChatClient.Commands;
using KChatClient.Enums;
using KChatClient.Models;
using KChatClient.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KChatClient.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
		private IChatService chatService;
		private IDialogService dialogService;
		private TaskFactory ctxTaskFactory;
		private const int MAX_IMAGE_WIDTH = 150;
		private const int MAX_IMAGE_HEIGHT = 150;

		private string _userName;
		public string UserName
		{
			get { return _userName; }
			set
			{
				_userName = value;
				OnPropertyChanged();
			}
		}

		private string _photo;
		public string Photo
		{
			get { return _photo; }
			set
			{
				_photo = value;
				OnPropertyChanged();
			}
		}

		private ObservableCollection<Participant> _participants = new ObservableCollection<Participant>();
		public ObservableCollection<Participant> Participants
		{
			get { return _participants; }
			set
			{
				_participants = value;
				OnPropertyChanged();
			}
		}

		private Participant _selectedParticipant;
		public Participant SelectedParticipant
		{
			get { return _selectedParticipant; }
			set
			{
				_selectedParticipant = value;
				if (SelectedParticipant.HasSentNewMessage) SelectedParticipant.HasSentNewMessage = false;
				SelectedUserName = SelectedParticipant.Name;
				OnPropertyChanged();
			}
		}

		private string _selectedUserName;
		public string SelectedUserName
		{
			get { return _selectedUserName; }
			set
			{
				_selectedUserName = value;
				OnPropertyChanged();
			}
		}

		private UserModes _userMode;
		public UserModes UserMode
		{
			get { return _userMode; }
			set
			{
				_userMode = value;
				OnPropertyChanged();
			}
		}

		private string _message;
		public string Message
		{
			get { return _message; }
			set
			{
				_message = value;
				OnPropertyChanged();
			}
		}

		private string _taskDescription;
		public string TaskDescription
		{
			get { return _taskDescription; }
			set
			{
				_taskDescription = value;
				OnPropertyChanged();
			}
		}

		private string _taskPriority;
		public string TaskPriority
		{
			get { return _taskPriority; }
			set
			{
				_taskPriority = value;
				OnPropertyChanged();
			}
		}

		private byte[] _file;
		public byte[] File
		{
			get { return _file; }
			set
			{
				_file = value;
				OnPropertyChanged();
			}
		}

		private bool _isConnected;
		public bool IsConnected
		{
			get { return _isConnected; }
			set
			{
				_isConnected = value;
				OnPropertyChanged();
			}
		}

		private bool _isLoggedIn;
		public bool IsLoggedIn
		{
			get { return _isLoggedIn; }
			set
			{
				_isLoggedIn = value;
				OnPropertyChanged();
			}
		}

		private ICommand _connectCommand;
		public ICommand ConnectCommand
		{
			get
			{
				if (_connectCommand == null) _connectCommand = new RelayCommandAsync(() => Connect());
				return _connectCommand;
			}
		}

		private async Task<bool> Connect()
		{
			try
			{
				await chatService.ConnectAsync();
				IsConnected = true;
				return true;
			}
			catch (Exception) { return false; }
		}

		private ICommand _loginCommand;
		public ICommand LoginCommand
		{
			get
			{
				if (_loginCommand == null) _loginCommand = new RelayCommandAsync(() => Login(), (o) => CanLogin());
				return _loginCommand;
			}
		}

		private async Task<bool> Login()
		{
			try
			{
				List<User> users = new List<User>();
				users = await chatService.LoginAsync(_userName, Avatar());

				if (users != null)
				{
					users.ForEach(u => Participants.Add(new Participant { Name = u.Name, Photo = u.Photo }));
					UserMode = UserModes.Chat;
					IsLoggedIn = true;
					return true;
				}
				else
				{
					dialogService.ShowNotification("Username is already in use");
					return false;
				}

			}
			catch (Exception) { return false; }
		}



		private bool CanLogin()
		{
			return !string.IsNullOrEmpty(UserName) && UserName.Length >= 2 && IsConnected;
		}

		private ICommand _logoutCommand;
		public ICommand LogoutCommand
		{
			get
			{
				if (_logoutCommand == null) _logoutCommand =
						new RelayCommandAsync(() => Logout(), (o) => CanLogout());
				return _logoutCommand;
			}
		}

		private async Task<bool> Logout()
		{
			try
			{
				await chatService.LogoutAsync();
				UserMode = UserModes.Login;
				return true;
			}
			catch (Exception) { return false; }
		}

		private bool CanLogout()
		{
			return IsConnected && IsLoggedIn;
		}

		private ICommand _sendMessageCommand;
		public ICommand SendMessageCommand
		{
			get
			{
				if (_sendMessageCommand == null) _sendMessageCommand =
						new RelayCommandAsync(() => SendMessage(), (o) => CanSendMessage());
				return _sendMessageCommand;
			}
		}

		private async Task<bool> SendMessage()
		{
			try
			{
				var recepient = _selectedParticipant.Name;
				await chatService.SendUnicastMessageAsync(recepient, _message);
				return true;
			}
			catch (Exception) { return false; }
			finally
			{
				ChatMessage msg = new ChatMessage
				{
					Author = UserName,
					Message = _message,
					Time = DateTime.Now,
					IsOriginNative = true
				};
				SelectedParticipant.Chatter.Add(msg);
				Message = string.Empty;
			}
		}

		private bool CanSendMessage()
		{
			return (!string.IsNullOrEmpty(Message) && IsConnected &&
				_selectedParticipant != null && _selectedParticipant.IsLoggedIn);
		}

		private ICommand _selectPhotoCommand;
		public ICommand SelectPhotoCommand
		{
			get
			{
				if (_selectPhotoCommand == null) _selectPhotoCommand = new RelayCommand((o) => SelectPhoto());
				return _selectPhotoCommand;
			}
		}

		private void SelectPhoto()
		{
			var pic = dialogService.OpenFile("Select image file", "Images (*.jpg;*.png)|*.jpg;*.png");
			if (!string.IsNullOrEmpty(pic))
			{
				var img = Image.FromFile(pic);
				if (img.Width > MAX_IMAGE_WIDTH || img.Height > MAX_IMAGE_HEIGHT)
				{
					dialogService.ShowNotification($"Image size should be {MAX_IMAGE_WIDTH} x {MAX_IMAGE_HEIGHT} or less.");
					return;
				}
				Photo = pic;
			}
		}

		private bool CanSendFile()
		{
			return (IsConnected && _selectedParticipant != null && _selectedParticipant.IsLoggedIn);
		}

		private ICommand _isSelectedUser;
		public ICommand IsSelectedUser
		{
			get
			{
				if (_isSelectedUser == null) _isSelectedUser =
						new RelayCommandAsync(() => SetNothing(), (o) => CanSendFile());
				return _setNewTaskCommand;
			}
		}

		private Task SetNothing()
		{
			return null;
		}

		private bool IsTheUserSelected()
		{
			return (_selectedParticipant != null);
		}

		private ICommand _setNewTaskCommand;
		public ICommand SetNewTaskCommand
		{
			get
			{
				if (_setNewTaskCommand == null) _setNewTaskCommand =
						new RelayCommandAsync(() => SetNewTask(), (o) => CanSendFile());
				return _setNewTaskCommand;
			}
		}

		private async Task<bool> SetNewTask()
		{
			try
			{
				var recepient = _selectedParticipant.Name;
				await chatService.SetNewTaskAsync(recepient, _taskDescription, _taskPriority);
				return true;
			}
			catch (Exception ex)
			{
				System.Windows.MessageBox.Show(ex.Message);
				return false;
			}
			finally
			{
				if (!string.IsNullOrEmpty(_taskDescription))
				{
					ChatMessage msg = new ChatMessage
					{
						Author = UserName,
						Message = $"You have new task from: {_userName}",
						Time = DateTime.Now,
						IsOriginNative = true
					};
					SelectedParticipant.Chatter.Add(msg);
					Message = string.Empty;
					TaskDescription = string.Empty;
					TaskPriority = string.Empty;
				}
			}
		}

		#region "Event handlers"
		private void NewMessage(string name, string msg, MessageType mt)
		{
			if (mt == MessageType.Unicast)
			{
				ChatMessage cm = new ChatMessage { Author = name, Message = msg, Time = DateTime.Now };
				var sender = _participants.Where((u) => string.Equals(u.Name, name)).FirstOrDefault();
				ctxTaskFactory.StartNew(() => sender.Chatter.Add(cm)).Wait();

				if (!(SelectedParticipant != null && sender.Name.Equals(SelectedParticipant.Name)))
				{
					ctxTaskFactory.StartNew(() => sender.HasSentNewMessage = true).Wait();
				}
			}
		}

		private void NewFileSend(string name, byte[] file)
		{
			System.Windows.MessageBox.Show("File was sent to you");
		}

		private void ParticipantLogin(User u)
		{
			var ptp = Participants.FirstOrDefault(p => string.Equals(p.Name, u.Name));
			if (_isLoggedIn && ptp == null)
			{
				ctxTaskFactory.StartNew(() => Participants.Add(new Participant
				{
					Name = u.Name,
					Photo = u.Photo
				})).Wait();
			}
		}

		private void ParticipantDisconnection(string name)
		{
			var person = Participants.Where((p) => string.Equals(p.Name, name)).FirstOrDefault();
			if (person != null) person.IsLoggedIn = false;
		}

		private void ParticipantReconnection(string name)
		{
			var person = Participants.Where((p) => string.Equals(p.Name, name)).FirstOrDefault();
			if (person != null) person.IsLoggedIn = true;
		}

		private void Reconnecting()
		{
			IsConnected = false;
			IsLoggedIn = false;
		}

		private async void Reconnected()
		{
			var pic = Avatar();
			if (!string.IsNullOrEmpty(_userName)) await chatService.LoginAsync(_userName, pic);
			IsConnected = true;
			IsLoggedIn = true;
		}

		private async void Disconnected()
		{
			var connectionTask = chatService.ConnectAsync();
			await connectionTask.ContinueWith(t =>
			{
				if (!t.IsFaulted)
				{
					IsConnected = true;
					chatService.LoginAsync(_userName, Avatar()).Wait();
					IsLoggedIn = true;
				}
			});
		}

		private void NewTask(string name, string taskDesc, string taskPriority)
		{
			var msg = $"You have new task: {taskDesc} \nFrom: {name}";
			dialogService.ShowToastNotification(msg);
			ChatMessage cm = new ChatMessage { Author = name, Message = msg, Time = DateTime.Now };
			var sender = _participants.Where((u) => string.Equals(u.Name, name)).FirstOrDefault();
			ctxTaskFactory.StartNew(() => sender.Chatter.Add(cm)).Wait();

			if (!(SelectedParticipant != null && sender.Name.Equals(SelectedParticipant.Name)))
			{
				ctxTaskFactory.StartNew(() => sender.HasSentNewMessage = true).Wait();
			}

		}

		private void ShowUserTasks(string name)
		{
			var msg = name;
			ChatMessage cm = new ChatMessage { Author = name, Message = msg, Time = DateTime.Now };
			var sender = _participants.Where((u) => string.Equals(u.Name, name)).FirstOrDefault();
			ctxTaskFactory.StartNew(() => sender.Chatter.Add(cm)).Wait();

			if (!(SelectedParticipant != null && sender.Name.Equals(SelectedParticipant.Name)))
			{
				ctxTaskFactory.StartNew(() => sender.HasSentNewMessage = true).Wait();
			}
		}
		#endregion

		private byte[] Avatar()
		{
			byte[] pic = null;
			if (!string.IsNullOrEmpty(_photo)) pic = System.IO.File.ReadAllBytes(_photo);
			return pic;
		}

		public MainWindowViewModel(IChatService chatSvc, IDialogService diagSvc)
		{
			dialogService = diagSvc;
			chatService = chatSvc;

			chatSvc.NewMessage += NewMessage;
			chatSvc.ParticipantLoggedIn += ParticipantLogin;
			chatSvc.ParticipantLoggedOut += ParticipantDisconnection;
			chatSvc.ParticipantDisconnected += ParticipantDisconnection;
			chatSvc.ParticipantReconnected += ParticipantReconnection;
			chatSvc.ConnectionReconnecting += Reconnecting;
			chatSvc.ConnectionReconnected += Reconnected;
			chatSvc.ConnectionClosed += Disconnected;
			chatSvc.SetNewTask += NewTask;

			ctxTaskFactory = new TaskFactory(TaskScheduler.FromCurrentSynchronizationContext());
		}

	}
}
