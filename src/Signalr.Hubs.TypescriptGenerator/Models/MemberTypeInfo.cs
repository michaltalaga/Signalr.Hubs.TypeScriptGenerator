using System;

namespace GeniusSports.Signalr.Hubs.TypeScriptGenerator.Models
{
	/// <summary>
	/// Describes representation of the named and typed code element (interface member property or function argument).
	/// </summary>
	public class MemberTypeInfo : MemberInfo
	{
		/// <summary>
		/// Name of the member described by this <see cref="MemberTypeInfo"/>,
		/// decorated with <i>optional</i> specifier (<c>?</c>) if <see cref="IsOptional"/> is <see langword="true"/>.
		/// </summary>
		public string DeclaredName => IsOptional ? Name + "?" : Name;

		/// <summary>
		/// Type of the code element as represented in the Type Script.
		/// </summary>
		public string TypeScriptType { get; }

		/// <summary>
		/// Indicates if the interface or member function argument described by this <see cref="MemberTypeInfo"/> should be
		/// declared as optional.
		/// </summary>
		public bool IsOptional { get; }

		public MemberTypeInfo(string name, string typescriptType)
			: this(name, false, null, typescriptType, false)
		{
		}

		/// <summary>
		/// Initializes new instance of<see cref="MemberTypeInfo"/> with supplied values.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="typescriptType"></param>
		/// <param name="isOptional"></param>
		/// <param name="isDeprecated"></param>
		/// <param name="reasonDeprecated"></param>
		public MemberTypeInfo(string name, bool isDeprecated, string reasonDeprecated, string typescriptType, bool isOptional)
			: base(name, isDeprecated, reasonDeprecated)
		{
			if (typescriptType == null)
				throw new ArgumentNullException(nameof(typescriptType));

			TypeScriptType = typescriptType;
			IsOptional = isOptional;
		}

		public override string ToString()
		{
			return string.Concat(base.ToString(), $"; TypeScriptType:{TypeScriptType}; IsOptional:{IsOptional}");
		}
	}
}