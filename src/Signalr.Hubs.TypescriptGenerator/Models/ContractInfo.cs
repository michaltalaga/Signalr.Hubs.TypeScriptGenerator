using System;

namespace GeniusSports.Signalr.Hubs.TypeScriptGenerator.Models
{
	/// <summary>
	/// Base class for different types of contracts. Provides common properties.
	/// </summary>
	public abstract class ContractInfo : MemberInfo
	{
		public string ModuleName { get; }

		/// <summary>
		/// Initializes new instance of<see cref="ContractInfo"/> with supplied values.
		/// </summary>
		/// <param name="name">Contract name.</param>
		/// <param name="isDeprecated">Indicates if the type system member is deprecated.</param>
		/// <param name="reasonDeprecated">text message describing the reason for the contract being marked as obsolete (optional).</param>
		/// <param name="moduleName">Module name.</param>
		protected ContractInfo(string name, bool isDeprecated, string reasonDeprecated, string moduleName) 
			: base(name, isDeprecated, reasonDeprecated)
		{
			if (moduleName == null)
				throw new ArgumentNullException(nameof(moduleName));

			ModuleName = moduleName;
		}

		public override string ToString()
		{
			return string.Concat(base.ToString(), $"; ModuleName:{ModuleName}");
		}
	}
}
