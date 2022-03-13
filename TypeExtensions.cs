using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Snap.Reflection
{
    public static class TypeExtensions
    {
        public static bool TryGetAttribute<T>(this Type type, [NotNullWhen(true)] out T? attribute) where T : Attribute
        {
            if (type.GetCustomAttribute<T>() is T attr)
            {
                attribute = attr;
                return true;
            }
            else
            {
                attribute = null;
                return false;
            }
        }

        public static bool Implement<TInterface>(this Type type)
        {
            return type.IsAssignableTo(typeof(TInterface));
        }
    }
}
