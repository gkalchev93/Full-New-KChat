using System.ServiceModel;

namespace KChatServer.WCF
{
    public interface IKClient
    {
        [OperationContract(IsOneWay = true)]
        void RecieveFile(string sender, byte[] file, string fileName);
    }
}
