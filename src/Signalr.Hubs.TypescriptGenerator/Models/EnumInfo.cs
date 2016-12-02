using System;
using System.Collections.Generic;
using System.Linq;

namespace GeniusSports.Signalr.Hubs.TypeScriptGenerator.Models
{
	public class EnumInfo : ContractInfo
	{
		public List<EnumMemberInfo> Members { get; }

		/// <summary>
		/// Initializes new instance of<see cref="EnumInfo"/> with supplied values.
		/// </summary>
		/// <param name="name">Contract name.</param>
		/// <param name="isDeprecated">Indicates if the type system member is deprecated.</param>
		/// <param name="reasonDeprecated">text message describing the reason for the contract being marked as obsolete (optional).</param>
		/// <param name="moduleName">Module name.</param>
		/// <param name="members"></param>
		public EnumInfo(
			string name, bool isDeprecated, string reasonDeprecated, string moduleName,
			List<EnumMemberInfo> members)
			: base(name, isDeprecated, reasonDeprecated, moduleName)
		{
			if (members == null)
				throw new ArgumentNullException(nameof(members));

			Members = members;
		}

		public override string ToString()
		{
			return string.Concat(base.ToString(), $"; Values:[{string.Join(",", Members.Select(m => m.ToString()))}]");
		}
	}
}