using System;
using GeniusSports.Signalr.Hubs.TypeScriptGenerator.SampleUsage.DataContracts;
using Microsoft.AspNet.SignalR;

namespace GeniusSports.Signalr.Hubs.TypeScriptGenerator.SampleUsage.Hubs
{
	[Obsolete("Superseded by IHubBClient.")]
	public interface IHubAClient
	{
		[Obsolete]
		void Pong();
		[Obsolete("for testing reasons.")]
		void TakeThis(SomethingDto somethingDto);
	}

	[Obsolete("Superseded by HubB.")]
	public class HubA : Hub<IHubAClient>
	{
		[Obsolete]
		public SomethingDto GetSomething()
		{
			return new SomethingDto();
		}

		public InheritedSomethingDto GetInheritedSomething()
		{
			return new InheritedSomethingDto();
		}

		[Obsolete("for testing reasons.")]
		public void Ping()
		{
		}
	}
}