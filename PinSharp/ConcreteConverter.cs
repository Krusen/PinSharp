using System;
using Newtonsoft.Json;

namespace PinSharp
{
    public class ConcreteConverter<T> : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize<T>(reader);
        }

        public override bool CanConvert(Type objectType) => true;
    }

    public class ConcreteConverter<TInterface, TType> : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize<TType>(reader);
        }

        public override bool CanConvert(Type objectType) => objectType == typeof(TInterface);
    }
}
