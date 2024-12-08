using Larkins.AdventOfCode.Utilities;

namespace Larkins.AdventOfCode.Tests.UtilityTests;

public class TextFileReaderTests
{
    /// <summary>
    /// While possible to use TestableIO (https://github.com/TestableIO/System.IO.Abstractions)
    /// Just use a real file initially.
    /// </summary>
    [Fact]
    public void Text_can_be_read_from_specified_file()
    {
        var reader = new TextFileReader();
        
        var result = reader.ReadAllTextInFile("some-text-file.txt");

        var expected = """
           Some text on a line
           another line
           
           one more.
           
           """;

        result.Should().Be(expected);
    }
}