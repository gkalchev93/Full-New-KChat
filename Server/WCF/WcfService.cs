using System.Collections.Generic;
using System.ServiceModel;

namespace KChatServer.WCF
{

    [ServiceContract(CallbackContract = typeof(IKClient), SessionMode = SessionMode.Required)]
    public interface IKWcfService
    {
        //    [OperationContract]
        //    void ReceiveFile(string fileName);

        [OperationContract]
        void SendFile(string sender, byte[] file, string filePath);

        [OperationContract]
        void Login(string username);
    }

    //[ServiceContract]
    public class KChatWcfService : IKWcfService
    {

        private readonly Dictionary<IKClient, string> _users = new Dictionary<IKClient, string>();

        //[OperationContract]
        public void Login(string userName)
        {
            var connection = OperationContext.Current.GetCallbackChannel<IKClient>();
            _users[connection] = userName;
        }

        //[OperationContract]
        //public void ReceiveFile(string fileName)
        //{
        //    try
        //    {
        //        // get some info about the input file
        //        string filePath = System.IO.Path.Combine(@"c:\Uploadfiles", fileName);
        //        System.IO.FileInfo fileInfo = new System.IO.FileInfo(filePath);

        //        // check if exists
        //        if (!fileInfo.Exists) throw new System.IO.FileNotFoundException("File not found", fileName);

        //        // open stream
        //        System.IO.FileStream stream = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);

        //        // return result
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}

        //[OperationContract]
        public void SendFile(string sender, byte[] file, string fileName)
        {
            //string uploadFolder = Path.Combine(Constants.appDataFolder, Constants.appFolder);
            //string filePath = Path.Combine(uploadFolder, fileName);

            //if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            //    Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            //File.WriteAllBytes(filePath, request);

            var connection = OperationContext.Current.GetCallbackChannel<IKClient>();
            if (!_users.TryGetValue(connection, out string user))
                return;

            foreach (var otherConnection in _users.Keys)
            {
                if (otherConnection == connection)
                    continue;
                otherConnection.RecieveFile(sender, file, fileName);
            }
        }
    }
}
