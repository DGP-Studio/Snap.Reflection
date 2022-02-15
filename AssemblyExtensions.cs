using System;
using System.Reflection;

namespace Snap.Reflection
{
    public static class AssemblyExtensions
    {
        public static bool HasAttribute<T>(this Assembly assembly) where T : Attribute
        {
            return assembly.GetCustomAttribute<T>() is not null;
        }

        public static void ForEachType(this Assembly assembly, Action<Type> action)
        {
            foreach (Type type in assembly.GetTypes())
            {
                action.Invoke(type);
            }
        }
    }
}
