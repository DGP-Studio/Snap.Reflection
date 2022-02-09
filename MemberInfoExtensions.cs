using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Snap.Reflection
{
    public static class MemberInfoExtensions
    {
        public static bool TryGetAttribute<T>(this MemberInfo type, [NotNullWhen(true)] out T? attribute) where T : Attribute
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

        public static void OnHaveAttribute<TAttribute>(this MemberInfo type, Action<TAttribute> action) where TAttribute : Attribute
        {
            if (type.TryGetAttribute(out TAttribute? attribute))
            {
                action(attribute);
            }
        }

        public static void OnNotHaveAttribute<TAttribute>(this MemberInfo type, Action action) where TAttribute : Attribute
        {
            if (!type.TryGetAttribute(out TAttribute? attribute))
            {
                action();
            }
        }
    }
}
