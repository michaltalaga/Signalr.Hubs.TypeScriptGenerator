using System;
using System.Runtime.Serialization;

namespace GeniusSports.Signalr.Hubs.TypeScriptGenerator.SampleUsage.DataContracts
{
	[Obsolete("Will be removed in beta version.")]
	[DataContract]
	public class InheritedSomethingDto : SomethingDto
	{
		[DataMember]
		public int OptionalInteger { get; set; }

		[DataMember(IsRequired = true)]
		public int? NullableInteger { get; set; }

		[Obsolete("This might be removed in next version.")]
		[DataMember(IsRequired = false)]
		public int? OptionalNullableInteger { get; set; }

	}
}