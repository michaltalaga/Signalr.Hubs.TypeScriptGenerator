using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GeniusSports.Signalr.Hubs.TypeScriptGenerator.Models
{
	public class ServiceInfo : ContractInfo
	{
		public string ClientType { get; }

		public string ServerType { get; }

		public string ServerFullType => string.Concat(ModuleName, ".", ServerType);

		public List<MethodInfo> Methods { get; }

		public ServiceInfo(
			string name, bool isDeprecated, string reasonDeprecated, string moduleName, 
			string clientType, string serverType, List<MethodInfo> methods)
			: base(name, isDeprecated, reasonDeprecated, moduleName)
		{
			ClientType = clientType;
			ServerType = serverType;
			Methods = methods;
		}

		public override string ToString()
		{
			return string.Concat(
				base.ToString(), 
				$"; ClientType:{ClientType}; ServerType: {ServerType}; Methods: {string.Join(", ", Methods.Select(m => m.Name))}");
		}
	}
}