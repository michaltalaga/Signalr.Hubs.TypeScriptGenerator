using System;
using System.Reflection;

namespace GeniusSports.Signalr.Hubs.TypeScriptGenerator.Helpers
{
    internal static class MemberInfoExtensions
    {
        public static bool IsDeprecated(this MemberInfo methodInfo, out string reasonDeprected)
        {
            if (methodInfo == null)
                throw new ArgumentNullException(nameof(methodInfo));

            var attr = methodInfo.GetCustomAttribute<ObsoleteAttribute>();
            if (attr != null)
            {
                reasonDeprected = attr.Message;
                return true;
            }

            reasonDeprected = null;
            return false;
        }
    }
}
