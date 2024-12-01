using FluentAssertions;
using Larkins.AdventOfCode.AdventOfCode2023.Day01;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2023.Day01;

public class Year2023Day01Part02Tests
{
    [Theory]
    [InlineData("1abc2", 12)]
    [InlineData("pqr3stu8vwx", 38)]
    [InlineData("a1b2c3d4e5f", 15)]
    [InlineData("treb7uchet", 77)]
    [InlineData("two1nine", 29)]
    [InlineData("eightwothree", 83)]
    [InlineData("abcone2threexyz", 13)]
    [InlineData("xtwone3four", 24)]
    [InlineData("4nineeightseven2", 42)]
    [InlineData("zoneight234", 14)]
    [InlineData("7pqrstsixteen", 76)]
    [InlineData("qgoneighteighttgrdmljtzbblrtskvfivevbbp1", 11)]
    [InlineData("7fiveonedzbmblrtqfoneightkc", 78)]
    public void Coordinate_is_the_first_and_last_digit_or_digit_word_of_each_line_combined(
        string inputLine,
        int expected)
    {
        var solver = new Year2023Day01Part02Solver();
        var result = solver.Solve([inputLine]);

        result.Should().Be(expected);
    }

    [Fact]
    public void Coordinate_is_the_first_and_last_digit_or_digit_word_of_each_line_combined_and_all_lines_summed()
    {
        var input = """
            two1nine
            eightwothree
            abcone2threexyz
            xtwone3four
            4nineeightseven2
            zoneight234
            7pqrstsixteen
            """;
        var inputLines = input.Split(Environment.NewLine);

        var solver = new Year2023Day01Part02Solver();
        var result = solver.Solve(inputLines);

        result.Should().Be(281);
    }
    
    [Fact]
    public void Test_sum()
    {
        var input = """
                    fivethreeonezblqnsfk1
                    two74119onebtqgnine
                    jrjh5vsrxbhsfour3
                    tn5eightfncnzcdtthree8
                    kpmrk5flx
                    fkxxqxdfsixgthreepvzjxrkcfk6twofour
                    dqbx6six5twoone
                    glmsckj2bvmts1spctnjrtqhmbxzq
                    7sixthreerzmpbffcx
                    zhss9gfxfgmrmzthreefivevpkljfourtwoeight
                    6tfzvrbkfour
                    sevenfive66five851
                    drsgdrrgscqmsggrgq1fsqjhtkkrltt
                    3ftptvzhvrm5
                    twoeightninemfsztp2gbqkpgqvzt6threekcdcp
                    four156
                    959157fourfive
                    sixthreetwo87one7fourdbczdbjcc
                    lshzfive7
                    38ninethreethreesevensixeight
                    """;
        var inputLines = input.Split(Environment.NewLine);

        var solver = new Year2023Day01Part02Solver();
        var result = solver.Solve(inputLines);

        result.Should().Be(1067);
    }
}