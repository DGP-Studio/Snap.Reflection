using System;
using System.Reflection;

namespace Snap.Reflection
{
    public static class ObjectExtensions
    {
        public static void ForEachProperty<T>(this T obj, Action<PropertyInfo> action)
        {
            foreach (PropertyInfo itemProperty in obj!.GetType().GetProperties())
            {
                action(itemProperty);
            }
        }
    }
}
