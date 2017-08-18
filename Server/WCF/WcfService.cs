using System.Collections.Generic;
using System.Data;
using System.ServiceModel;

namespace KChatServer.WCF
{

    [ServiceContract(CallbackContract = typeof(IKClient), SessionMode = SessionMode.Required)]
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

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
    ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    public class KChatWcfService : IKWcfService
    {

        private readonly Dictionary<IKClient, string> _users = new Dictionary<IKClient, string>();

        public IKClient CurrentCallback
        {
            get
            {
                return OperationContext.Current.GetCallbackChannel<IKClient>();
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
