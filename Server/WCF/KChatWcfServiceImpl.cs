using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace KChatServer.WCF
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
	ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
	public class KChatWcfServiceImpl : IKWcfService
	{

		private readonly Dictionary<IKWcfClient, string> _users = new Dictionary<IKWcfClient, string>();

		public IKWcfClient CurrentCallback
		{
			get
			{
				return OperationContext.Current.GetCallbackChannel<IKWcfClient>();
			}
		}

		public void Login(string userName)
		{
			if (!_users.ContainsValue(userName) && !_users.ContainsKey(CurrentCallback))
			{
				System.Console.Write("++ " + userName + " logged in WCF service");
				_users.Add(CurrentCallback, userName);
			}
		}

		public void Logout()
		{
			if (_users.ContainsKey(CurrentCallback))
			{
				System.Console.WriteLine("-- " + _users[CurrentCallback] + " logged out from WCF service");
				_users.Remove(CurrentCallback);
			}
		}

		public void SendFile(string receiver, byte[] file, string fileName)
		{
			string sender = string.Empty;
			if (!_users.ContainsKey(CurrentCallback))
				return;

			if (!_users.TryGetValue(CurrentCallback, out sender))
				return;

			foreach (var user in _users)
			{
				if (user.Value == sender)
					continue;
				else if (user.Value == receiver)
					user.Key.RecieveFile(sender, file, fileName);
			}
		}

		public DataTable GetTasks(string user)
		{
			return DbHelper.SelectUserTasks(user);
		}
	}
}
