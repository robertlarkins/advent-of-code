using Larkins.AdventOfCode.Models;

namespace Larkins.AdventOfCode.Tests.ModelTests;

public class LineTests
{
    public static TheoryData<Line, double> LineSlopeScenarios() => new()
    {
        { new Line(new GridPoint(0, 0), new GridPoint(0, 2)), 0D },
        { new Line(new GridPoint(0, 0), new GridPoint(2, 2)), 1D },
        { new Line(new GridPoint(1, 1), new GridPoint(-1, -1)), 1D },
        { new Line(new GridPoint(2, 0), new GridPoint(0, 1)), -2D },
    };

    public static TheoryData<Line, double> VerticalLineSlopeScenarios() => new()
    {
        { new Line(new GridPoint(1, 1), new GridPoint(-1, 1)), double.PositiveInfinity },
        { new Line(new GridPoint(0, 0), new GridPoint(2, 0)), double.PositiveInfinity },
    };

    public static TheoryData<Line, Line, List<GridPoint>> LineIntersectionScenarios() => new()
    {
        {
            new Line(new GridPoint(2, 0), new GridPoint(2, 4)),
            new Line(new GridPoint(0, 2), new GridPoint(4, 2)),
            [new GridPoint(2, 2)]
        },
        {
            new Line(new GridPoint(0, 2), new GridPoint(4, 2)),
            new Line(new GridPoint(2, 0), new GridPoint(2, 4)),
            [new GridPoint(2, 2)]
        },
        {
            new Line(new GridPoint(0, 0), new GridPoint(4, 4)),
            new Line(new GridPoint(4, 0), new GridPoint(0, 4)),
            [new GridPoint(2, 2)]
        },
        {
            new Line(new GridPoint(-4, -3), new GridPoint(0, -1)),
            new Line(new GridPoint(4, 0), new GridPoint(4, 4)),
            [new GridPoint(4, 1)]
        },
    };

    public static TheoryData<Line, Line, List<GridPoint>> NonIntersectingLineScenarios() => new()
    {
        {
            new Line(new GridPoint(0, 0), new GridPoint(1, 1)),
            new Line(new GridPoint(0, 2), new GridPoint(1, 3)),
            []
        },
        {
            new Line(new GridPoint(0, 0), new GridPoint(0, 1)),
            new Line(new GridPoint(1, 0), new GridPoint(1, 3)),
            []
        },
        {
            new Line(new GridPoint(0, 0), new GridPoint(4, 0)),
            new Line(new GridPoint(1, 2), new GridPoint(10, 2)),
            []
        },
    };

    public static TheoryData<Line, Line, List<GridPoint>> CollinearLineScenarios() => new()
    {
        {
            new Line(new GridPoint(0, 0), new GridPoint(4, 4)),
            new Line(new GridPoint(2, 2), new GridPoint(6, 6)),
            [new GridPoint(2, 2), new GridPoint(4, 4)]
        },
    };

    [Fact]
    public void LineCreation()
    {
        var point1 = new GridPoint(1, 2);
        var point2 = new GridPoint(4, 3);

        var sut = new Line(point1, point2);

        using (new AssertionScope())
        {
            sut.Point1.Should().Be(point1);
            sut.Point2.Should().Be(point2);
        }
    }

    [Theory]
    [MemberData(nameof(LineIntersectionScenarios))]
    [MemberData(nameof(NonIntersectingLineScenarios))]
    [MemberData(nameof(CollinearLineScenarios))]
    public void IntersectingLines(Line line1, Line line2, List<GridPoint> expected)
    {
        var result = line1.Intersect(line2);

        result.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [MemberData(nameof(LineSlopeScenarios))]
    [MemberData(nameof(VerticalLineSlopeScenarios))]
    public void LineSlope(Line sut, double expected)
    {
        sut.Slope.Should().BeApproximately(expected, TestConstants.DoublePrecision);
    }

    [Fact]
    public void VerticalLineYIntersectPointIsNaN()
    {
        var sut = new Line(new GridPoint(0, 0), new GridPoint(2, 0));

        sut.YIntercept().Should().BeNaN();
    }

    [Fact]
    public void HorizontalLineXIntersectPointIsNaN()
    {
        var sut = new Line(new GridPoint(0, 0), new GridPoint(0, 2));

        sut.XIntercept().Should().BeNaN();
    }

    [Fact]
    public void LineYIntersectPoint()
    {
        var sut = new Line(new GridPoint(5, -5), new GridPoint(5, 5));

        var expected = 5D;

        sut.YIntercept().Should().Be(expected);
    }

    [Fact]
    public void VerticalLineXIntersectPoint()
    {
        var sut = new Line(new GridPoint(5, -5), new GridPoint(-5, -5));

        var expected = -5D;

        sut.XIntercept().Should().Be(expected);
    }

    [Fact]
    public void LineXIntersectPoint()
    {
        var sut = new Line(new GridPoint(1, -5), new GridPoint(-1, -3));

        var expected = -4D;

        sut.XIntercept().Should().Be(expected);
    }

    [Fact]
    public void TestColGivenRow()
    {
        var sut = new Line(new GridPoint(0, 0), new GridPoint(4, 2));

        var result = sut.GetColGivenRow(2);

        result.Should().Be(1);
    }

    [Fact]
    public void TestRowGivenCol()
    {
        var sut = new Line(new GridPoint(0, 0), new GridPoint(4, 2));

        var result = sut.GetRowGivenCol(1);

        result.Should().Be(2);
    }
}
