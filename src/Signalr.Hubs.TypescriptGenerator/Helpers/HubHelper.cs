using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GeniusSports.Signalr.Hubs.TypeScriptGenerator.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace GeniusSports.Signalr.Hubs.TypeScriptGenerator.Helpers
{
	internal class HubHelper
	{
		private readonly DefaultHubManager hubmanager;
		private readonly TypeHelper typeHelper;


		public HubHelper(TypeScriptGeneratorOptions options)
		{
			typeHelper = new TypeHelper(options);

			var defaultDependencyResolver = new DefaultDependencyResolver();
			hubmanager = new DefaultHubManager(defaultDependencyResolver);
		}

		public List<MemberTypeInfo> GetHubs()
		{
			return hubmanager
				.GetHubs()
				.Select(hub =>
				{
					string reasonDeprecated;
					bool isDeprecated = hub.HubType.IsDeprecated(out reasonDeprecated);
					return new MemberTypeInfo(
						hub.NameSpecified ? hub.Name : typeHelper.FirstCharLowered(hub.Name),
						isDeprecated, reasonDeprecated, hub.HubType.FullName, false);
				}).ToList();
	}

		public List<ServiceInfo> GetServiceContracts()
		{
			var list = new List<ServiceInfo>();

			foreach (var hub in hubmanager.GetHubs())
			{
				var hubMethods = hubmanager.GetHubMethods(hub.Name).Select(GetServiceMethod).ToList();

				var hubType = hub.HubType;
				var clientType = typeHelper.ClientType(hubType);

				string reasonDeprecated;
				bool isDeprecated = hub.HubType.IsDeprecated(out reasonDeprecated);

				list.Add(new ServiceInfo(
					hubType.Name, isDeprecated, reasonDeprecated, hubType.Namespace,
					clientType != null ? clientType.FullName : "any",
					hubType.Name + "Server", hubMethods));
			}

			return list;
		}

		private Models.MethodInfo GetServiceMethod(MethodDescriptor method)
		{
			var methodParametersString = string.Join(", ", method.Parameters
				.Select(typeHelper.GetParameterInfo)
				.Select(x => $"{x.DeclaredName} : {x.TypeScriptType}"));

			string reasonDeprecated;
			bool isDeprecated = method.IsDeprecated(out reasonDeprecated);

			return new Models.MethodInfo(
				typeHelper.FirstCharLowered(method.Name), isDeprecated, reasonDeprecated,
				$"({methodParametersString})",
				$"JQueryPromise<{typeHelper.GetTypeContractName(method.ReturnType)}>");
		}

		public List<ClientInfo> GetClients()
		{
			var list = new List<ClientInfo>();

			foreach (var hub in hubmanager.GetHubs())
			{
				var hubType = hub.HubType;
				var clientType = typeHelper.ClientType(hubType);
				if (clientType != null)
				{
					list.Add(new ClientInfo(
						clientType.Name, clientType.Namespace, typeHelper.GetClientMethods(hubType)));
				}
			}

			return list;
		}

		public List<DataContractInfo> GetDataContracts()
		{
			var list = new List<DataContractInfo>();

			while (typeHelper.InterfaceTypes.Count != 0)
			{
				var type = typeHelper.InterfaceTypes.Dequeue();
				var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
					.Select(prop => typeHelper.GetPropertyInfo(prop)).ToList();
				var baseType  = type.BaseType;
				var bases = typeHelper.IsRootBaseType(baseType) 
					? new string[0] 
					: new[] { typeHelper.GenericSpecificName(baseType, true) };

				string reasonDeprecated;
				bool isDeprecated = type.IsDeprecated(out reasonDeprecated);

				list.Add(new DataContractInfo(
					typeHelper.GenericSpecificName(type, false), isDeprecated, reasonDeprecated, type.Namespace, 
					bases, properties));

				typeHelper.DiscoverAdditionalTypes(type);
			}

			return list;
		}

		public List<EnumInfo> GetEnums()
		{
			var list = new List<EnumInfo>();

			foreach (var type in typeHelper.EnumTypes)
			{
				var enumProperties = Enum.GetNames(type).Select(memberName => GetEnumMemberInfo(type, memberName)).ToList();

				string reasonDeprecated;
				bool isDeprecated = type.IsDeprecated(out reasonDeprecated);

				list.Add(new EnumInfo(
					typeHelper.GenericSpecificName(type, false), isDeprecated, reasonDeprecated, type.Namespace, 
					enumProperties));
			}

			return list;
		}

		private static EnumMemberInfo GetEnumMemberInfo(Type enumType, string memberName)
		{
			var enumMember = enumType.GetField(memberName);

			string reasonDeprecated;
			bool isDeprecated = enumMember.IsDeprecated(out reasonDeprecated);

			return new EnumMemberInfo(
				memberName, isDeprecated, reasonDeprecated,
				enumMember.GetRawConstantValue());
		}
	}
}