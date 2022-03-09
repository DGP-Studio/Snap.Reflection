using System;
using System.Reflection;

namespace Snap.Reflection
{
    public static class ObjectExtensions
    {
        #region ForEachProperty

        public static void ForEachPropertyInfo(this object obj, Action<PropertyInfo> action)
        {
            foreach (PropertyInfo propInfo in obj.GetType().GetProperties())
            {
                action(propInfo);
            }
        }
        public static void ForEachPropertyInfoWithAttribute<TAttribute>(this object obj, Action<PropertyInfo, TAttribute> action) where TAttribute : Attribute
        {
            obj.ForEachPropertyInfo(propInfo =>
            propInfo.OnHaveAttribute<TAttribute>(attribute =>
            action(propInfo, attribute)));
        }
        public static void ForEachPropertyInfoWithoutAttribute<TAttribute>(this object obj, Action<PropertyInfo> action) where TAttribute : Attribute
        {
            obj.ForEachPropertyInfo(propInfo =>
            propInfo.OnNotHaveAttribute<TAttribute>(() =>
            action(propInfo)));
        }
        public static void ForEachPropertyWithAttribute<TAttribute>(this object obj, Action<object, TAttribute> action) where TAttribute : Attribute
        {
            obj.ForEachPropertyInfo(propInfo =>
            propInfo.OnHaveAttribute<TAttribute>(attribute =>
            action(obj.GetPropertyByInfo(propInfo)!, attribute)));
        }
        public static void ForEachPropertyWithAttribute<TProperty, TAttribute>(this object obj, Action<TProperty, TAttribute> action) where TProperty : class where TAttribute : Attribute
        {
            obj.ForEachPropertyInfo(propInfo =>
            propInfo.OnHaveAttribute<TAttribute>(attribute =>
            action(obj.GetPropertyByInfo<TProperty>(propInfo)!, attribute)));
        }
        #endregion

        #region ForEachAttribute
        public static void ForEachAttribute<TAttribute>(this object obj, Action<TAttribute> action) where TAttribute : Attribute
        {
            foreach (TAttribute attribute in obj.GetType().GetCustomAttributes<TAttribute>())
            {
                action(attribute);
            }
        }
        #endregion
        public static object? GetPropertyByInfo(this object obj, PropertyInfo propInfo)
        {
            return propInfo.GetValue(obj);
        }
        public static T? GetPropertyByInfo<T>(this object obj, PropertyInfo propInfo) where T : class
        {
            return propInfo.GetValue(obj) as T;
        }

        public static object? GetPropertyValueByInfo(this object obj, PropertyInfo propInfo)
        {
            return propInfo.GetValue(obj, null);
        }

        public static PropertyInfo GetPropertyInfoByName(this object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName)!;
        }

        public static T GetPropertyValueByName<T>(this object obj, string propertyName)
        {
            return (T)(obj.GetType().GetProperty(propertyName)!.GetValue(obj)!);
        }

        public static void SetPropertyValueByName(this object obj, string propertyName, object? value)
        {
            PropertyInfo? childProp = obj.GetPropertyInfoByName(propertyName);
            if (childProp?.CanWrite == true)
            {
                childProp.SetValue(obj, value, null);
            }
        }

        public static void SetPrivateFieldValueByName(this object obj, string fieldName, object? value)
        {
            FieldInfo? fieldInfo = obj.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
            fieldInfo?.SetValue(obj, value);
        }
    }
}
