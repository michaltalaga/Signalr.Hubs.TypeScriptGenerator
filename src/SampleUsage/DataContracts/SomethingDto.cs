using System;
using System.Runtime.Serialization;

namespace GeniusSports.Signalr.Hubs.TypeScriptGenerator.SampleUsage.DataContracts
{
	[DataContract]
	public class SomethingDto
	{
		[DataMember(Name = "iChangedTheName")]
		public string Property1 { get; set; }

		[DataMember(Name = "requiredString", IsRequired = true)]
		public string RequiredString { get; set; }

		[DataMember]
		public Guid OptionalGuid { get; set; }

		[DataMember(IsRequired = true)]
		public Guid RequiredGuid { get; set; }

		[DataMember]
		public InnerSomethingDto OptionalInnerSomething { get; set; }
	}
}