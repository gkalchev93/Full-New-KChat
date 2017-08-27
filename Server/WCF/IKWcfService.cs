using System.Collections.Generic;
using System.Data;
using System.ServiceModel;

namespace KChatServer.WCF
{

	[ServiceContract(CallbackContract = typeof(IKWcfClient), SessionMode = SessionMode.Required)]
	public interface IKWcfService
	{

		[OperationContract]
		void SendFile(string sender, byte[] file, string filePath);

		[OperationContract]
		void Login(string username);

		[OperationContract]
		void Logout();

		[OperationContract]
		DataTable GetTasks(string user);
	}
}
