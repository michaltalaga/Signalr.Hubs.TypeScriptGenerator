using System;
using System.Linq;
using Microsoft.AspNet.SignalR.Hubs;

namespace GeniusSports.Signalr.Hubs.TypeScriptGenerator.Helpers
{
    internal static class MethodDescriptorExtensions
    {
        public static bool IsDeprecated(this MethodDescriptor method, out string reasonDeprected)
        {
            if (method == null)
                throw new ArgumentNullException(nameof(method));

            var attr = method.Attributes.OfType<ObsoleteAttribute>().FirstOrDefault();
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
