using System;
using System.IO;
using System.Reflection;
using CommandLine;

namespace GeniusSports.Signalr.Hubs.TypeScriptGenerator.Console
{
	public class Program
	{
		public static int Main(string[] args)
		{
			if (AppDomain.CurrentDomain.IsDefaultAppDomain())
			{
				return RunInNewAppDomainToAllowRazorEngineToCleanup(args);
			}

			try
			{
				var options = new CommandLineOptions();
				if (Parser.Default.ParseArguments(args, options))
				{
					Run(options);
					return 0;
				}

				System.Console.WriteLine("Error parsing command line options");
				System.Console.WriteLine(options.GetUsage());
				System.Console.ReadKey();
				return 1;
			}
			catch (Exception e)
			{
				System.Console.WriteLine("Error generating TypeScript");
				System.Console.WriteLine(e);
				return 1;
			}
		}

		private static int RunInNewAppDomainToAllowRazorEngineToCleanup(string[] args)
		{
			var appDomain = AppDomain.CreateDomain("RazorEngine", null, AppDomain.CurrentDomain.SetupInformation);
			var exitCode = appDomain.ExecuteAssembly(Assembly.GetExecutingAssembly().Location, args);
			AppDomain.Unload(appDomain);
			return exitCode;
		}

		private static void Run(CommandLineOptions commandLineOptions)
		{
			LoadAssemblies(commandLineOptions.AssemblyPath);

			var generatorOptions = GetTypeScriptGeneratorOptions(commandLineOptions);
			var hubTypeScriptGenerator = new HubTypeScriptGenerator();
			var outputText = hubTypeScriptGenerator.Generate(generatorOptions);

			if (string.IsNullOrWhiteSpace(commandLineOptions.OutFile))
			{
				System.Console.WriteLine(outputText);
			}
			else
			{
				File.WriteAllText(commandLineOptions.OutFile, outputText);
			}
		}

		private static TypeScriptGeneratorOptions GetTypeScriptGeneratorOptions(CommandLineOptions commandLineOptions)
		{
			var options = TypeScriptGeneratorOptions.Default
				.WithReferencePaths(commandLineOptions.IncludeReferencePaths)
				.WithOptionalMembers(commandLineOptions.GetOptionalMemberGenerationMode());
			if (commandLineOptions.StrictTypes)
				options = options.WithStrictTypes(commandLineOptions.GetNotNullableTypeDiscovery());
			return options;
		}

		private static void LoadAssemblies(string assemblyPath)
		{
			var assemblyLoader = new AssemblyLoader();
			assemblyLoader.LoadAssemblyIntoAppDomain(assemblyPath);
		}
	}
}