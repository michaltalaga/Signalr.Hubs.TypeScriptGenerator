using System.Runtime.Serialization;

namespace GeniusSports.Signalr.Hubs.TypeScriptGenerator.SampleUsage.DataContracts
{
    [DataContract]
    public class InheritedSomethingDto : SomethingDto
    {
        [DataMember]
		public int OptionalInteger { get; set; }

		[DataMember(IsRequired = true)]
		public int? NullableInteger { get; set; }

		[DataMember(IsRequired = false)]
		public int? OptionalNullableInteger { get; set; }

	}
}