namespace Uncas.NowSite.Utilities
{
    public interface IStringSerializer
    {
        string Serialize(object data);
        T Deserialize<T>(string data);
    }
}