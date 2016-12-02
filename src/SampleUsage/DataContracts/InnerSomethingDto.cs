using System;
using System.Runtime.Serialization;

namespace GeniusSports.Signalr.Hubs.TypeScriptGenerator.SampleUsage.DataContracts
{
	[DataContract]
	[KnownType("GetKnownTypes")]
	public class InnerSomethingDto
	{
		[DataMember]
		public int InnerPropertyInt { get; set; }

		[DataMember(Name = "innerProperty2")]
		public DateTime InnerPropertyDate { get; set; }

		[Obsolete("Do not use properties with crazy names.")]
		[DataMember(Name = "innerProperty3WithCrazyCustomName")]
		public SomethingEnum InnerPropertyEnum { get; set; }

		[DataMember(Name = "inner123")]
		public object Inner { get; set; }

		static Type[] GetKnownTypes()
		{
			return new[]
			{
				typeof(InnerSomethingDto1),
				typeof(InnerSomethingDto2),
				typeof(InnerSomethingDto3)
			};
		}
	}

	[DataContract]
	public class InnerSomethingDto1
	{
		[DataMember]
		public InnerSomethingDto4 Dto2 { get; set; }
	}

	[DataContract]
	public class InnerSomethingDto2
	{
		[DataMember]
		public InnerSomethingDto4 Dto4 { get; set; }
	}

	[DataContract]
	public class InnerSomethingDto3
	{
		[DataMember]
		public int InnerProperty3 { get; set; }
	}

	[DataContract]
	public class InnerSomethingDto4
	{
		[DataMember]
		public InnerSomethingDto5 Dto5 { get; set; }
	}


	[DataContract]
	public class InnerSomethingDto5
	{
		[DataMember]
		public InnerSomethingDto3 Dto3 { get; set; }

		[DataMember(IsRequired = true)]
		public Enum5 Enum5 { get; set; }
	}

	[DataContract]
	public enum Enum5
	{
		[EnumMember]
		None,
		[EnumMember]
		Five
	}
}