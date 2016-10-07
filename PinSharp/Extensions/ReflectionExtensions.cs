using System;
using System.Collections.Generic;
using System.Reflection;

namespace PinSharp.Extensions
{
    internal static class ReflectionExtensions
    {
        public static IEnumerable<PropertyInfo> GetProperties(this Type type)
        {
#if NETSTANDARD
            return type.GetTypeInfo().DeclaredProperties;
#else
            return type.GetProperties();
#endif
        }

        public static bool IsInterface(this Type type)
        {
#if NETSTANDARD
            return type.GetTypeInfo().IsInterface;
#else
            return type.IsInterface;
#endif
        }

#if NETSTANDARD
        public static bool IsAssignableFrom(this Type type, Type otherType)
        {
            return type.GetTypeInfo().IsAssignableFrom(otherType.GetTypeInfo());
        }
#endif
    }
}
