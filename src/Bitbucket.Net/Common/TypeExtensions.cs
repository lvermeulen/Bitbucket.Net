using System;
using System.Reflection;

namespace Bitbucket.Net.Common
{
    public static class TypeExtensions
    {
        public static bool IsNullableType(Type type) => type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
    }
}
