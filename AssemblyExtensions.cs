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
    }
}
