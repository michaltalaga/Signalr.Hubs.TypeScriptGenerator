namespace GeniusSports.Signalr.Hubs.TypeScriptGenerator
{
	/// <summary>
	/// Defines methods to discover members that should be treated as not-nullable.
	/// </summary>
	public enum NotNullableTypeDiscovery
	{
		/// <summary>
		/// Treat members as nullable where applicable (<see cref="System.Nullable{T}"/> and reference types).
		/// </summary>
		None,

		/// <summary>
		/// Treat members attrobiuted with <see cref="System.ComponentModel.DataAnnotations.RequiredAttribute"/>
		/// as not-nullable.
		/// </summary>
		UseRequiredAttribute
	}
}