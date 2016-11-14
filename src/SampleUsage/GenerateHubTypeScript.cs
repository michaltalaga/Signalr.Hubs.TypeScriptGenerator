using NUnit.Framework;

namespace GeniusSports.Signalr.Hubs.TypeScriptGenerator.SampleUsage
{
	[TestFixture]
	public class GenerateHubTypeScript
	{
		[Test]
		public void Generate()
		{
			var hubTypeScriptGenerator = new HubTypeScriptGenerator();
#pragma warning disable 618
			var typeScript = hubTypeScriptGenerator.Generate(true);
#pragma warning restore 618
			System.Console.WriteLine(typeScript);
		}

		[Test]
		public void GenerateWithStrictTypesAndOptionalMembers()
		{
			var hubTypeScriptGenerator = new HubTypeScriptGenerator();
			var options = TypeScriptGeneratorOptions.Default
				.WithReferencePaths()
				.WithStrictTypes(NotNullableTypeDiscovery.UseRequiredAttribute)
				.WithOptionalMembers(OptionalMemberGenerationMode.UseDataMemberAttribute);
			var typeScript = hubTypeScriptGenerator.Generate(options);
			System.Console.WriteLine(typeScript);
		}
	}
}