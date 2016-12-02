using System.Collections.Generic;
using System.Linq;

namespace GeniusSports.Signalr.Hubs.TypeScriptGenerator.Models
{
	public class DataContractInfo : ContractInfo
	{
		public string[] BaseInterfaces { get; }

		public List<MemberTypeInfo> Properties { get; }

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

		public DataContractInfo(
			string name, bool isDeprecated, string reasonDeprecated, string moduleName,
			string[] baseInterfaces, List<MemberTypeInfo> properties)
			: base(name, isDeprecated, reasonDeprecated, moduleName)
		{
			BaseInterfaces = baseInterfaces;
			Properties = properties;
		}

		public override string ToString()
		{
			return string.Concat(base.ToString(), $"; Bases:{BaseInterfaces}; Properties:[{string.Join(",", Properties.Select(p => p.Name))}]");
		}
	}
}