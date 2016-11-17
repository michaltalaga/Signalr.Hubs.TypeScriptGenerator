using System.Collections.Generic;

namespace GeniusSports.Signalr.Hubs.TypeScriptGenerator.Models
{
	public class DataContractInfo
	{
		public string ModuleName { get; }

		public string InterfaceName { get; }

		public string[] BaseInterfaces { get; }

		/// <summary>
		/// Gets "extends" specification for the contract interface, if <see cref="BaseInterfaces"/> collection is not empty;
		/// empty string otherwise.
		/// </summary>
		public string ExtendsDeclaration
		{
			get
			{
				if (BaseInterfaces == null || BaseInterfaces.Length == 0)
					return string.Empty;
				return string.Concat("extends ", string.Join(", ", BaseInterfaces));
			}
		}

		public List<MemberTypeInfo> Properties { get; }

		public DataContractInfo(string moduleName, string interfaceName, string[] baseInterfaces, List<MemberTypeInfo> properties)
		{
			ModuleName = moduleName;
			InterfaceName = interfaceName;
			BaseInterfaces = baseInterfaces;
			Properties = properties;
		}

		public override string ToString()
		{
			return $"ModuleName:{ModuleName},InterfaceName:{InterfaceName};Bases:{BaseInterfaces};Properties:[{string.Join(",", Properties)}]";
		}
	}
}