namespace Uncas.NowSite.Web.Utilities
{
    public interface IStringSerializer
    {
        string Serialize(object data);
        T Deserialize<T>(string data);
    }
}