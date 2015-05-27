using TechDemo.Interface.Client;

namespace TechDemo.Interface.Server
{
    public interface ISocketServer
    {
        byte[] GenerateBytes(AbsDataModel[] dataModels);
    }
}
