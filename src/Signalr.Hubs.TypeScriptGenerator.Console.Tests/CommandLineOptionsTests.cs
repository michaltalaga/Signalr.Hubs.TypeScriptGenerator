using GeniusSports.Signalr.Hubs.TypeScriptGenerator.Console;
using NUnit.Framework;

namespace Signalr.Hubs.TypeScriptGenerator.Console.Tests
{
    [TestFixture]
    public class CommandLineOptionsTests
    {
        private const string TestAssemblyPath = @"c:\signalr\hubs\test\assembly.dll";

        private CommandLineOptions GetSUT(string assemblyPath = TestAssemblyPath)
        {
            return new CommandLineOptions()
            {
                AssemblyPath = assemblyPath
            };
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void GetOutputPath_WhenOutputIsNullOrEmpty_ThenReturnsNullOrEmpty(string output)
        {
            // Arrange
            var sut = GetSUT();
            sut.Output = output;

            // Act
            var outputPath = sut.GetOutputPath();

            // Assert
            Assert.That(outputPath, Is.EqualTo(output));
        }

        [Test]
        public void GetOutputPath_WhenOutputIsNotDirectory_ReturnsTheSuppliedValue()
        {
            // Arrange
            var sut = GetSUT();
            var suppliedOutput = "supplied output";
            sut.Output = suppliedOutput;

            // Act
            var outputPath = sut.GetOutputPath();

            // Assert
            Assert.That(outputPath, Is.EqualTo(suppliedOutput));
        }

        [Test]
        [TestCase(@".\", @".\assembly.d.ts")]
        [TestCase(@"\", @"\assembly.d.ts")]
        [TestCase(@"c:\ts\", @"c:\ts\assembly.d.ts")]
        public void GetOutputPath_WhenOutputIsDirectory_ThenGeneratesOutputPathFromAssembly(
            string suppliedOutput, string expectedOutput)
        {
            // Arrange
            var sut = GetSUT();
            sut.Output = suppliedOutput;

            // Act
            var outputPath = sut.GetOutputPath();

            // Assert
            Assert.That(outputPath, Is.EqualTo(expectedOutput));
        }
    }
}
