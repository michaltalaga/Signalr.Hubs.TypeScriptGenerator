using System;
using System.IO;
using CommandLine;
using CommandLine.Text;

namespace GeniusSports.Signalr.Hubs.TypeScriptGenerator.Console
{
	public class CommandLineOptions
	{
		[Option('a', "assembly", HelpText = "The path to the assembly (.dll/.exe)", Required = true)]
		public string AssemblyPath { get; set; }

		[Option('o', "output", HelpText = "The path to the file to generate. If this is empty, the output is written to stdout.")]
		public string Output { get; set; }

		[Option('r', "references", HelpText = "Optional. List of file paths, delimited by semicolon to be added as the <reference/> instruction.", Required = false)]
		public string References { get; set; }

		[Option('p', "optionalMembers", HelpText = "Specifies method to discover members treated as optional: None - don't generate optional members; DataMemberAttribute - use [DataMember(IsRequired)] attribute.", Required = false, DefaultValue = "None")]
		public string OptionalMembers { get; set; }

		[Option('s', "strictTypes", HelpText = "If true, union definitions with 'null' are generated for nullable types.", Required = false)]
		public bool StrictTypes { get; set; }

		[Option('n', "notNullableTypes", HelpText = "Specifies method to discover members treated as not-nullable: RequiredAttribute: - use [Required] attribute.", Required = false)]
		public string NotNullableTypes { get; set; }

		[Option("includeSourceDetails", HelpText = "Adds details on source assembly (name, version) to the header comment of the generated files.", DefaultValue = false, Required = false)]
		public bool IncludeSourceDetails { get; set; }

		[HelpOption]
		public string GetUsage()
		{
			return HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));
		}

		public OptionalMemberGenerationMode GetOptionalMemberGenerationMode()
		{
			if (string.IsNullOrEmpty(OptionalMembers))
				return OptionalMemberGenerationMode.None;

			switch (OptionalMembers.ToLowerInvariant())
			{
				case "none":
					return OptionalMemberGenerationMode.None;

				case "datamemberattribute":
					return OptionalMemberGenerationMode.UseDataMemberAttribute;

				default:
					throw new NotSupportedException($"Specified required member discovery option is not supported: {OptionalMembers}");
			}
		}

		public NotNullableTypeDiscovery GetNotNullableTypeDiscovery()
		{
			if (string.IsNullOrEmpty(NotNullableTypes))
				return NotNullableTypeDiscovery.None;

			switch (NotNullableTypes.ToLowerInvariant())
			{
				case "none":
					return NotNullableTypeDiscovery.None;

				case "requiredattribute":
					return NotNullableTypeDiscovery.UseRequiredAttribute;

				default:
					throw new NotSupportedException($"Specified required member discovery option is not supported: {NotNullableTypes}");
			}
		}

		/// <summary>
		/// Analyzes specified output path, and returnes adjusted/generateed output file path.
		/// </summary>
		public string GetOutputPath()
		{
			if (string.IsNullOrWhiteSpace(Output) || Output[Output.Length - 1] != Path.DirectorySeparatorChar)
				return Output;

			// Treat output path as directory, and generate output file name.

			var assemblyFileName = Path.GetFileNameWithoutExtension(AssemblyPath);
			return Path.Combine(Output, assemblyFileName + ".d.ts");
		}
	}
}