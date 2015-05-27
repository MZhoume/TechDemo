namespace TechDemo.Interface.Server
{
    public interface IServiceFactory
    {
        AbsDataService CreateService(int id, string str);
    }
}
