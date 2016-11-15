using System;
using System.Collections.Generic;

namespace GeniusSports.Signalr.Hubs.TypeScriptGenerator.Models
{
	public class TypesModel
	{
		public List<MemberTypeInfo> Hubs { get; }
		public List<ServiceInfo> ServiceContracts { get; }
		public List<ClientInfo> Clients { get; }
		public List<DataContractInfo> DataContracts { get; }
		public List<EnumInfo> Enums { get; }

		public DateTime LastGenerated => DateTime.UtcNow;
		public bool IncludeReferencePaths { get; set; }

		public TypesModel(
			List<MemberTypeInfo> hubs, 
			List<ServiceInfo> serviceContracts, 
			List<ClientInfo> clients, 
			List<DataContractInfo> dataContracts, 
			List<EnumInfo> enums)
		{
			Hubs = hubs;
			ServiceContracts = serviceContracts;
			Clients = clients;
			DataContracts = dataContracts;
			Enums = enums;
		}
	}
}