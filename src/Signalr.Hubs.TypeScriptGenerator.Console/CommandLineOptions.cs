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

		[Option('e', "exports", HelpText = "The path to the generated file containing exported code. If this is empty, name is generated from the output file.")]
		public string Exports { get; set; }

		[Option('i', "includeReferencePaths", HelpText = "If true, the jquery and signalr typings reference paths will be included.", DefaultValue = false, Required = false)]
		public bool IncludeReferencePaths { get; set; }

		[Option('p', "optionalMembers", HelpText = "Specifies method to discover members treated as optional: None - don't generate optional members; DataMemberAttribute - use [DataMember(IsRequired)] attribute.", Required = false, DefaultValue = "None")]
		public string OptionalMembers { get; set; }

		[Option('s', "strictTypes", HelpText = "If true, union definitions with 'null' are generated for nullable types.", Required = false)]
		public bool StrictTypes { get; set; }

		[Option('n', "notNullableTypes", HelpText = "Specifies method to discover members treated as not-nullable: RequiredAttribute: - use [Required] attribute.", Required = false)]
		public string NotNullableTypes { get; set; }

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

		private const string DeclarationFileExtension = ".d.ts";
		private const string TypescriptFileExtension = ".ts";
		private const string ExportsSuffix = ".exports";

		/// <summary>
		/// Analyzes output paths specified in command line options, and adjusts/generates file path as needed.
		/// </summary>
		public void AdjustOutputPaths()
		{
			if (string.IsNullOrWhiteSpace(Output))
			{
				Output = null;
				Exports = null;
				return;
			}

			if (Output[Output.Length - 1] == Path.DirectorySeparatorChar)
			{
				// Treat output path as directory, and generate output file name

				var assemblyFileName = Path.GetFileNameWithoutExtension(AssemblyPath);
				Output = Path.Combine(Output, Path.ChangeExtension(assemblyFileName, DeclarationFileExtension));
			}

			if (string.IsNullOrWhiteSpace(Exports))
			{
				// Generate from output file.

				foreach (var knownExtension in new[] { DeclarationFileExtension, TypescriptFileExtension } )
				{
					if (Output.EndsWith(knownExtension, StringComparison.InvariantCultureIgnoreCase))
					{
						Exports = string.Concat(Output.Substring(0, Output.Length - knownExtension.Length), ExportsSuffix, TypescriptFileExtension);
						return;
					}
				}

				var extension = Path.GetExtension(Output);
				if (!string.IsNullOrEmpty(extension))
				{
					Exports = Path.ChangeExtension(Output, ExportsSuffix + extension);
				}
				else
				{
					Exports = Output + ExportsSuffix;
				}
			}
		}
	}
}