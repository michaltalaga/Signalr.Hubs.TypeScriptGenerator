namespace GeniusSports.Signalr.Hubs.TypeScriptGenerator.Models
{
	/// <summary>
	/// Describes representation of the named and typed code element (interface member property or function argument).
	/// </summary>
	public class MemberTypeInfo
	{
		/// <summary>
		/// Name of the member described by this <see cref="MemberTypeInfo"/>.
		/// </summary>
		public string Name { get; }

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
			: this(name, typescriptType, false)
		{
		}

		public MemberTypeInfo(string name, string typescriptType, bool isOptional)
		{
			Name = name;
			TypeScriptType = typescriptType;
			IsOptional = isOptional;
		}

		public override string ToString()
		{
			return $"{DeclaredName} : {TypeScriptType}";
		}
	}
}