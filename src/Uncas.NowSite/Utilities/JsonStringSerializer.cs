using System;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;

namespace Uncas.NowSite.Utilities
{
    public class JsonStringSerializer : IStringSerializer
    {
        private static readonly JsonSerializer Serializer =
            JsonSerializer.Create(new JsonSerializerSettings());

        public string Serialize(object data)
        {
            TextWriter writer = new StringWriter(CultureInfo.InvariantCulture);
            Serializer.Serialize(writer, data);
            return writer.ToString();
        }

        public T Deserialize<T>(string data)
        {
            Type type = typeof(T);
            return (T)Serializer.Deserialize(
                new StringReader(data), type);
        }
    }
}