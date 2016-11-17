using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GeniusSports.Signalr.Hubs.TypeScriptGenerator.Models
{
	public class TypesModel
	{
		public ICollection<string> ReferencePaths { get; }
		public ICollection<MemberTypeInfo> Hubs { get; }
		public ICollection<ServiceInfo> ServiceContracts { get; }
		public ICollection<ClientInfo> Clients { get; }
		public ICollection<DataContractInfo> DataContracts { get; }
		public ICollection<EnumInfo> Enums { get; }

		public TypesModel(
			IEnumerable<string> referencePaths,
			IEnumerable<MemberTypeInfo> hubs,
			IEnumerable<ServiceInfo> serviceContracts,
			IEnumerable<ClientInfo> clients,
			IEnumerable<DataContractInfo> dataContracts,
			IEnumerable<EnumInfo> enums)
		{
			ReferencePaths = new ReadOnlyCollection<string>(referencePaths.ToArray());
			Hubs = new ReadOnlyCollection<MemberTypeInfo>(hubs.ToArray());
			ServiceContracts = new ReadOnlyCollection<ServiceInfo>(serviceContracts.ToArray());
			Clients = new ReadOnlyCollection<ClientInfo>(clients.ToArray());
			DataContracts = new ReadOnlyCollection<DataContractInfo>(dataContracts.ToArray());
			Enums = new ReadOnlyCollection<EnumInfo>(enums.ToArray());
		}
	}
}