using System.Reflection;

namespace Snap.Reflection
{
    public static class AssemblyNameExtensions
    {
        /// <summary>
        /// 返回程序集名称的版本号字符串
        /// 当程序集版本未知时返回 <see cref="string.Empty"/>
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        public static string GetVersionString(this AssemblyName assemblyName)
        {
            return assemblyName.Version?.ToString() ?? string.Empty;
        }
    }
}
