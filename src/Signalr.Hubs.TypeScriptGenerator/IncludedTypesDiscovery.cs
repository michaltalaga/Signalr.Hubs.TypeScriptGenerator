namespace GeniusSports.Signalr.Hubs.TypeScriptGenerator
{
	/// <summary>
	/// Defines methods to discover types that should be included in the generated type script (when not used 
	/// directly in the Hub interface method arguments).
	/// <para>This is useful for example, to generate interfaces for derived types, when base type references are 
	/// passed to Hub methods.</para>
	/// </summary>
	public enum IncludedTypesDiscovery
	{
		/// <summary>
		/// Do not include any types other than directly used in Hub interface methods. (Default.)
		/// </summary>
		None,

		/// <summary>
		/// Generate interfaces for types declared in the [KnownType] attribute in any of types directly used in 
		/// Hub methods.
		/// </summary>
		UseKnownTypeAttribute
	}
}