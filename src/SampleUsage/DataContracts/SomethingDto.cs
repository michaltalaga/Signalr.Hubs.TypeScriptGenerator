using System;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
#pragma warning disable 618

namespace GeniusSports.Signalr.Hubs.TypeScriptGenerator.SampleUsage.DataContracts
{
	[DataContract]
	[KnownType(typeof(InheritedSomethingDto))]
	public class SomethingDto
	{
		[DataMember(Name = "iChangedTheName")]
		public string Property1 { get; set; }

		[DataMember(Name = "requiredString", IsRequired = true)]
		public string RequiredString { get; set; }

		[DataMember]
		public Guid? OptionalGuid { get; set; }

		[DataMember(IsRequired = true)]
		public Guid RequiredGuid { get; set; }

		[DataMember(IsRequired = true)]
		[Required]
		public virtual Guid? NullableRequiredGuid { get; set; }

		[DataMember]
		[Required]
		public InnerSomethingDto OptionalInnerSomething { get; set; }
	}
}