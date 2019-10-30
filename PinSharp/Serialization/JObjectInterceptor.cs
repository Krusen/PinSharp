using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace PinSharp.Serialization
{
    //public static class JObjectExtension
    //{
    //    private static ProxyGenerator _generator = new ProxyGenerator();

    //    public static dynamic ToProxy(this JObject targetObject, Type interfaceType, IContractResolver contractResolver)
    //    {
    //        return _generator.CreateInterfaceProxyWithoutTarget(interfaceType, new JObjectInterceptor(targetObject, interfaceType, contractResolver));
    //    }

    //    public static T ToProxy<T>(this JObject targetObject, IContractResolver contractResolver)
    //    {
    //        return ToProxy(targetObject, typeof(T), contractResolver);
    //    }
    //}

    [Serializable]
    internal class JObjectInterceptor : IInterceptor
    {
        private JObject Target { get; }

        private JsonPropertyCollection PropertyCollection { get; }

        private JsonSerializer Serializer { get; }

        private InterfaceProxyConverter ProxyConverter { get; }

        //private IContractResolver ContractResolver { get; }

        public JObjectInterceptor(JObject target, JsonPropertyCollection propertyCollection, JsonSerializer serializer, InterfaceProxyConverter proxyConverter)
        {
            ProxyConverter = proxyConverter ?? throw new ArgumentNullException(nameof(proxyConverter));
            Target = target ?? throw new ArgumentNullException(nameof(target));
            Serializer = serializer;
            PropertyCollection = propertyCollection;
        }

        //public JObjectInterceptor(JObject target, Type targetType, IContractResolver contractResolver)
        //{
        //    Target = target ?? throw new ArgumentNullException(nameof(target));
        //    ContractResolver = contractResolver;
        //    PropertyCollection = (ContractResolver.ResolveContract(targetType) as JsonObjectContract).Properties;
        //}

        public void Intercept(IInvocation invocation)
        {
            var methodName = invocation.Method.Name;
            if (invocation.Method.IsSpecialName && methodName.StartsWith("get_"))
            {
                var returnType = invocation.Method.ReturnType;
                methodName = methodName.Substring(4);
                //methodName = GetPropertyName(methodName);

                // TODO: Refactor/clean up
                var value =
                    Target[methodName] ??
                    Target.GetValue(methodName, StringComparison.OrdinalIgnoreCase) ??
                    Target[PropertyCollection.GetClosestMatchProperty(methodName)?.PropertyName ?? ""] ??
                    Target[PropertyCollection.FirstOrDefault(x => x.UnderlyingName == methodName)?.PropertyName ?? ""];

                if (value == null)
                {
                    if (returnType.GetTypeInfo().IsPrimitive || returnType == typeof(string))
                    {
                        invocation.ReturnValue = null;
                        return;
                    }

                    // TODO: Unsure what we are doing here
                    invocation.ReturnValue = null;
                    return;
                }

                // TODO: Maybe move this as last 'else', and add inverse check in current 'else-if'
                if (returnType.FullName.StartsWith("System."))
                {
                    invocation.ReturnValue = value.ToObject(returnType);
                }
                else if (returnType.IsInterface && value is JObject jobject)
                {

                    invocation.ReturnValue = ProxyConverter.CreateProxy(jobject, returnType, Serializer);
                    //invocation.ReturnValue = jobject.ToProxy(returnType, ContractResolver);
                }
                else
                {
                    throw new Exception($"Unhandled value of type {value.GetType()}: {value}");
                }

                return;
            }



            // TODO: Implement?
            throw new NotImplementedException("Only get accessors are implemented in proxy");
        }

        //public void Intercept3(IInvocation invocation)
        //{
        //    var methodName = invocation.Method.Name;
        //    if (invocation.Method.IsSpecialName && methodName.StartsWith("get_"))
        //    {
        //        var returnType = invocation.Method.ReturnType;
        //        methodName = methodName.Substring(4);

        //        var value = Target[methodName] ??
        //                    Target.GetValue(methodName, StringComparison.OrdinalIgnoreCase) ??
        //                    Target[PropertyCollection.GetClosestMatchProperty(methodName)?.PropertyName ?? ""] ??
        //                    Target[PropertyCollection.FirstOrDefault(x => x.UnderlyingName == methodName)?.PropertyName ?? ""];

        //        if (value == null)
        //        {
        //            if (returnType.GetTypeInfo().IsPrimitive || returnType == typeof(string))
        //            {
        //                invocation.ReturnValue = null;
        //                return;
        //            }

        //            // TODO: Unsure what we are doing here
        //            invocation.ReturnValue = null;
        //            return;
        //        }

        //        // TODO: Maybe move this as last 'else', and add inverse check in current 'else-if'
        //        if (returnType.FullName.StartsWith("System."))
        //        {
        //            invocation.ReturnValue = value.ToObject(returnType);
        //        }
        //        else if (returnType.IsInterface && value is JObject jobject)
        //        {
        //            invocation.ReturnValue = jobject.ToProxy(returnType, ContractResolver);
        //        }
        //        else
        //        {
        //            throw new Exception($"Unhandled value of type {value.GetType()}: {value}");
        //        }

        //        return;
        //    }



        //    // TODO: Implement?
        //    throw new NotImplementedException("Only get accessors are implemented in proxy");
        //}
    }
}
