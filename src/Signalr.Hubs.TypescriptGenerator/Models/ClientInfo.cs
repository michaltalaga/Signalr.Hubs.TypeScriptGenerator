using System.Collections.Generic;
using System.Linq;

namespace GeniusSports.Signalr.Hubs.TypeScriptGenerator.Models
{
	public class ClientInfo : ContractInfo
	{
		public List<MethodInfo> Methods { get; }

		public ClientInfo(string name, string moduleName, List<MethodInfo> methods)
			: base(name, false, null, moduleName)
		{
			Methods = methods;
		}

		public override string ToString()
		{
			return string.Concat(base.ToString(), $"; Methods:{string.Join(", ", Methods.Select(m => m.Name))}");
		}
	}
}