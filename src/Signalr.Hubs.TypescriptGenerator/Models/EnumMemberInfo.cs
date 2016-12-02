using System;
using System.Collections.Generic;
using System.Linq;

namespace GeniusSports.Signalr.Hubs.TypeScriptGenerator.Models
{
	public class EnumMemberInfo : MemberInfo
	{
		public object Value { get; }

		/// <summary>
		/// Initializes new instance of<see cref="EnumInfo"/> with supplied values.
		/// </summary>
		/// <param name="name">Enum member name.</param>
		/// <param name="isDeprecated">Indicates if the enum member is deprecated.</param>
		/// <param name="reasonDeprecated">text message describing the reason for the enum member being marked as obsolete (optional).</param>
		/// <param name="value">Enum member value.</param>
		public EnumMemberInfo(
			string name, bool isDeprecated, string reasonDeprecated, object value)
			: base(name, isDeprecated, reasonDeprecated)
		{
			Value = value;
		}

		public override string ToString()
		{
			return string.Concat(base.ToString(), $"; Value:{Value}");
		}
	}
}