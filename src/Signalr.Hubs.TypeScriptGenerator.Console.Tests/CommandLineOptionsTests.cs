using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        [TestCase(null, null)]
        [TestCase(null, "")]
        [TestCase(null, " ")]
        [TestCase("", null)]
        [TestCase("", "")]
        [TestCase("", " ")]
        [TestCase(" ", null)]
        [TestCase(" ", "")]
        [TestCase(" ", " ")]
        public void AdjustOutputPaths_WhenOutputIsEmpty_ThenResetsBothPathsToNull(string output, string exports)
        {
            // Arrange
            var sut = GetSUT();
            sut.Output = output;
            sut.Exports = exports;

            // Act
            sut.AdjustOutputPaths();

            // Assert
            Assert.IsNull(sut.Output);
            Assert.IsNull(sut.Exports);
        }

        [Test]
        public void AdjustOutputPaths_WhenPathsAreSpecified_DoesNotAlter()
        {
            // Arrange
            var sut = GetSUT();
            var suppliedOutput = "supplied output";
            var suppliedExports = "supplied exports";
            sut.Output = suppliedOutput;
            sut.Exports = suppliedExports;

            // Act
            sut.AdjustOutputPaths();

            // Assert
            Assert.That(sut.Output, Is.EqualTo(suppliedOutput));
            Assert.That(sut.Exports, Is.EqualTo(suppliedExports));
        }

        [Test]
        [TestCase(@".\", @".\assembly.d.ts", @".\assembly.exports.ts")]
        [TestCase(@"\", @"\assembly.d.ts", @"\assembly.exports.ts")]
        [TestCase(@"c:\ts\", @"c:\ts\assembly.d.ts", @"c:\ts\assembly.exports.ts")]
        public void AdjustOutputPaths_WhenOutputIsDirectory_ThenGeneratesOutputPathFromAssembly(
            string suppliedOutput, string expectedOutput, string expectedExports)
        {
            // Arrange
            var sut = GetSUT();
            sut.Output = suppliedOutput;

            // Act
            sut.AdjustOutputPaths();

            // Assert
            Assert.That(sut.Output, Is.EqualTo(expectedOutput));
            Assert.That(sut.Exports, Is.EqualTo(expectedExports));
        }

        [Test]
        [TestCase(@"c:\ts\assembly.d.ts", @"c:\ts\assembly.exports.ts")]
        [TestCase(@"c:\ts\assembly.ts", @"c:\ts\assembly.exports.ts")]
        [TestCase(@"assembly.txt", @"assembly.exports.txt")]
        [TestCase(@"assembly", @"assembly.exports")]
        public void AdjustOutputPaths_WhenOutputIsDirectoryAndExportsIsEmpty_GeneratesExportsFromSuppliedOutput(
            string suppliedOutput, string expectedExports)
        {
            // Arrange
            var sut = GetSUT();
            sut.Output = suppliedOutput;

            // Act
            sut.AdjustOutputPaths();

            // Assert
            Assert.That(sut.Exports, Is.EqualTo(expectedExports));
        }

    }
}
