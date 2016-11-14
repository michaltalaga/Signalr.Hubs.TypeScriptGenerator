using System;
using System.IO;
using System.Reflection;
using GeniusSports.Signalr.Hubs.TypeScriptGenerator.Helpers;
using GeniusSports.Signalr.Hubs.TypeScriptGenerator.Models;
using RazorEngine;
using RazorEngine.Templating;

namespace GeniusSports.Signalr.Hubs.TypeScriptGenerator
{
	public class HubTypeScriptGenerator
	{
		[Obsolete("This method is superseded with Generate(TypeScriptGeneratorOptions) method.")]
		public string Generate(bool includeReferencePaths = false)
		{
			return Generate(TypeScriptGeneratorOptions.Default.WithReferencePaths(includeReferencePaths));
		}

		public string Generate(TypeScriptGeneratorOptions options)
		{
			var model = GenerateTypeScriptModel(options);
			model.IncludeReferencePaths = options.IncludeReferencePaths;
			var template = ReadEmbeddedFile("template.cshtml");
			var outputText = Engine.Razor.RunCompile(template, "templateKey", null, model);
			return outputText;
		}

		private static TypesModel GenerateTypeScriptModel(TypeScriptGeneratorOptions options)
		{
			var signalrHelper = new HubHelper(options);
			return new TypesModel(
				signalrHelper.GetHubs(),
				signalrHelper.GetServiceContracts(),
				signalrHelper.GetClients(),
				signalrHelper.GetDataContracts(),
				signalrHelper.GetEnums());
		}

		private static string ReadEmbeddedFile(string file)
		{
			string resourcePath = $"{typeof(HubTypeScriptGenerator).Namespace}.{file}";

			using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath))
			{
				if (stream == null) throw new InvalidOperationException($"Unable to find '{resourcePath}' as an embedded resource");

				string textContent;
				using (var reader = new StreamReader(stream))
				{
					textContent = reader.ReadToEnd();
				}

				return textContent;
			}
		}
	}
}