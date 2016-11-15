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
		/// <summary>
		/// Generates typescript declarations and exports code.
		/// </summary>
		/// <param name="options">The parsed command line options.</param>
		/// <returns>The <see cref="Tuple{string, string}"/> with <see cref="Tuple{string, string}.Item1"/> set to 
		/// generated declarations, and the <see cref="Tuple{string, string}.Item2"/> set to generated exports.
		/// </returns>
		public Tuple<string, string> Generate(TypeScriptGeneratorOptions options)
		{
			var model = GenerateTypeScriptModel(options);
			model.IncludeReferencePaths = options.IncludeReferencePaths;
			var declarationsTemplate = ReadEmbeddedFile("declarations.cshtml");
			var exportsTemplate = ReadEmbeddedFile("exports.cshtml");
			var declaration = Engine.Razor.RunCompile(declarationsTemplate, "declarationKey", null, model);
			var exports = Engine.Razor.RunCompile(exportsTemplate, "exportsKey", null, model);
			return Tuple.Create(declaration, exports);
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