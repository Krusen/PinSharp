using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PinSharp.Serialization
{
    public class InterfaceConverter<T> : JsonConverter where T : class, new()
    {
        //public override bool CanConvert(Type objectType) => objectType.IsInterface() && objectType.IsAssignableFrom(typeof(T));
        public override bool CanConvert(Type objectType) => false;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = new T();
            var objectReader = JObject.Load(reader).CreateReader();
            serializer.Populate(objectReader, value);
            return value;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
