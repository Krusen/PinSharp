using System;
using System.Collections;
using System.Collections.Generic;
using Castle.DynamicProxy;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using PinSharp.Extensions;

namespace PinSharp.Serialization
{
    public class InterfaceProxyConverter : JsonConverter
    {
        private ProxyGenerator ProxyGenerator { get; } = new ProxyGenerator();

        // TODO: Can we do without IEnumerable check?
        public override bool CanConvert(Type objectType) => objectType.IsInterface() && !typeof(IEnumerable).IsAssignableFrom(objectType);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);
            //if (token.Type == JTokenType.Array)
            //    return token.ToObject(objectType);

            if (token.Type == JTokenType.Object)
            {
                var jobject = token as JObject;
                if (jobject == null)
                    return null;

                // TODO: Use extension method like this, or pass proxy generator/generator func to interceptor?
                //return jobject.ToProxy(objectType, serializer.ContractResolver);

                return CreateProxy(jobject, objectType, serializer);

                //var interceptor = new JObjectInterceptor(jobject, objectType, serializer.ContractResolver);
                //return ProxyGenerator.CreateInterfaceProxyWithoutTarget(objectType, interceptor);
            }

            return null;
        }

        public object CreateProxy(JObject jobject, Type targetType, JsonSerializer serializer)
        {
            var contract = serializer.ContractResolver.ResolveContract(targetType) as JsonObjectContract;
            var interceptor = new JObjectInterceptor(jobject, contract.Properties, serializer, this);
            return ProxyGenerator.CreateInterfaceProxyWithoutTarget(targetType, interceptor);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}