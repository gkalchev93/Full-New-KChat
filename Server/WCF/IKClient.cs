using System.ServiceModel;

namespace KChatServer.WCF
{
    public interface IKWcfClient
    {
        [OperationContract(IsOneWay = true)]
        void RecieveFile(string sender, byte[] file, string fileName);
    }
}
