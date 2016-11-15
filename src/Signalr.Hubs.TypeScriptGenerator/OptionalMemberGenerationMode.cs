namespace GeniusSports.Signalr.Hubs.TypeScriptGenerator
{
	/// <summary>
	/// Defines methods to discover members which should be treated as required and not marked with
	/// <i>optional</i> specifier (the <b>?</b> in TypeScript).
	/// </summary>
	public enum OptionalMemberGenerationMode
	{
		/// <summary>
		/// Don't generate optional members.
		/// </summary>
		None,

		/// <summary>
		/// Generate members attributed by <see cref="System.Runtime.Serialization.DataMemberAttribute"/>
		/// having <see cref="System.Runtime.Serialization.DataMemberAttribute.IsRequired"/> property set to 
		/// <see langword="false"/> (default value) as optional.
		/// </summary>
		UseDataMemberAttribute
	};
}