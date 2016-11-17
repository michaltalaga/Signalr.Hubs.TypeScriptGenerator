namespace GeniusSports.Signalr.Hubs.TypeScriptGenerator
{
	/// <summary>
	/// Encapsulates options controlling valrious aspects of Type Script code generation.
	/// </summary>
	/// <remarks>
	/// To construct the instance, use the <see cref="Default"/> member to access the instance with default options.
	/// Use fluent members to construct customized settings.
	/// </remarks>
	public sealed class TypeScriptGeneratorOptions
	{
		/// <summary>
		/// Semicolon-delimited file paths added as <c>&lt;reference /&gt;</c> instructions to the declarations file.
		/// </summary>
		public string[] ReferencePaths { get; }

		/// <summary>
		/// Indicates if and how optional contract interface members shall be generated.
		/// </summary>
		public OptionalMemberGenerationMode OptionalMemberGenerationMode { get; }

		/// <summary>
		/// Instructs to generate strict type definitions by explicitly adding <c>null</c> type to the member type 
		/// declaration for nullable members. 
		/// <para>
		/// This option is useful when TypeScipt is expected to be compiled using the <b>"--strictNullChecks"</b> 
		/// compiler option.
		/// </para>
		/// </summary>
		public bool GenerateStrictTypes { get; }

		/// <summary>
		/// Identifies the method used to discover members that should not be declared as <i>nullable</i>. 
		/// This option is only applicable when <see cref="GenerateStrictTypes"/> is <see langword="true"/>; 
		/// ignored otherwise.
		/// </summary>
		public NotNullableTypeDiscovery NotNullableTypeDiscovery { get; }

		private TypeScriptGeneratorOptions()
		{
		}

		private TypeScriptGeneratorOptions(
			string[] referencePaths,
			OptionalMemberGenerationMode optionalMemberGenerationMode, 
			bool generateStrictTypes, 
			NotNullableTypeDiscovery notNullableTypeDiscovery)
		{
			ReferencePaths = referencePaths;
			OptionalMemberGenerationMode = optionalMemberGenerationMode;
			GenerateStrictTypes = generateStrictTypes;
			NotNullableTypeDiscovery = notNullableTypeDiscovery;	
		}

		/// <summary>
		/// Returns the instance of <see cref="TypeScriptGeneratorOptions"/> initialized with default options.
		/// By defualt, strict nullable types are not generated, and interface members are never declared as optional.
		/// </summary>
		public static readonly TypeScriptGeneratorOptions Default = new TypeScriptGeneratorOptions();

		public TypeScriptGeneratorOptions WithReferencePaths(params string [] referencePaths)
		{
			return new TypeScriptGeneratorOptions(
				referencePaths,
				OptionalMemberGenerationMode,
				GenerateStrictTypes,
				NotNullableTypeDiscovery);
		}

		public TypeScriptGeneratorOptions WithOptionalMembers(
			OptionalMemberGenerationMode requiredMemberDiscovery)
		{
			return new TypeScriptGeneratorOptions(
				ReferencePaths,
				requiredMemberDiscovery,
				GenerateStrictTypes,
				NotNullableTypeDiscovery);
		}

		public TypeScriptGeneratorOptions WithStrictTypes(
			NotNullableTypeDiscovery notNullableTypeDiscovery = NotNullableTypeDiscovery.None)
		{
			return new TypeScriptGeneratorOptions(
				ReferencePaths,
				OptionalMemberGenerationMode,
				true,
				notNullableTypeDiscovery);
		}
	}
}
