using System.Collections.Generic;

namespace GeniusSports.Signalr.Hubs.TypeScriptGenerator.Models
{
	public class EnumInfo
	{
		public string ModuleName { get; }
		public string InterfaceName { get; }
		public List<MemberTypeInfo> Properties { get; }

		public EnumInfo(string moduleName, string interfaceName, List<MemberTypeInfo> properties)
		{
			ModuleName = moduleName;
			InterfaceName = interfaceName;
			Properties = properties;
		}

		public override string ToString()
		{
			return $"ModuleName:{ModuleName},InterfaceName:{InterfaceName};Properties:[{string.Join(",", Properties)}]";
		}
	}
}