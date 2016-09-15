using GeniusSports.Signalr.Hubs.TypescriptGenerator.SampleUsage.DataContracts;
using Microsoft.AspNet.SignalR;

namespace GeniusSports.Signalr.Hubs.TypescriptGenerator.SampleUsage.Hubs
{
    public interface IHubAClient
    {
        void Pong();
        void TakeThis(SomethingDto somethingDto);
    }

    public class HubA : Hub<IHubAClient>
    {
        public SomethingDto GetSomething()
        {
            return new SomethingDto();
        }

        public void Ping()
        {
        }
    }
}