namespace Larkins.AdventOfCode.Models;

public record Line
{
    public Line(
        GridPoint point1,
        GridPoint point2)
    {
        Point1 = point1;
        Point2 = point2;
        Slope = CalculateSlope();
    }

    public GridPoint Point1 { get; }

    public GridPoint Point2 { get; }

    public double Slope { get; }

    /// <summary>
    /// Returns the Intersect point between two lines. The List will be empty if they do not overlap
    /// one GridPoint if a single overlap, or two points (start and end of a line) for where
    /// collinear lines overlap.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public List<GridPoint> Intersect(Line other)
    {
        // Are both lines vertical
        var isLineVertical = double.IsPositiveInfinity(Slope);
        var isOtherLineVertical = double.IsPositiveInfinity(other.Slope);

        // are lines vertical? Either collinear or intersect
        if (isLineVertical && isOtherLineVertical)
        {
            if (Math.Abs(XIntercept() - other.XIntercept()) > Constants.DoublePrecision)
            {
                return [];
            }

            return OverlapOfCollinearLines(this, other);
        }

        // lines have same slope. Either collinear or intersect
        if (Math.Abs(Slope - other.Slope) < Constants.DoublePrecision)
        {
            if (Math.Abs(YIntercept() - other.YIntercept()) > Constants.DoublePrecision)
            {
                return [];
            }

            return OverlapOfCollinearLines(this, other);
        }

        // if either line is vertical
        if (double.IsPositiveInfinity(Slope))
        {
            var intersectRow = (int)other.GetRowGivenCol(Point1.Col);
            return [Point1 with { Row = intersectRow }];
        }

        if (double.IsPositiveInfinity(other.Slope))
        {
            var intersectRow = (int)GetRowGivenCol(other.Point1.Col);
            return [other.Point1 with { Row = intersectRow }];
        }

        var yIntercept = YIntercept();
        var yInterceptOther = other.YIntercept();

        var interceptCol = (int)Math.Round((yInterceptOther - yIntercept) / (Slope - other.Slope));
        var interceptRow = (int)Math.Round(Slope * interceptCol + yIntercept);

        return [new GridPoint(interceptRow, interceptCol)];

        static List<GridPoint> OverlapOfCollinearLines(Line line1, Line line2)
        {
            // Need to deal with horizontal
            if (double.IsPositiveInfinity(line1.Slope) && double.IsPositiveInfinity(line2.Slope))
            {
                var highestLine1Point = line1.Point1.Row < line1.Point2.Row? line1.Point2 : line1.Point1;
                var lowestLine1Point = line1.Point1.Row < line1.Point2.Row? line1.Point1 : line1.Point2;

                var highestLine2Point = line2.Point1.Row < line2.Point2.Row? line2.Point2 : line2.Point1;
                var lowestLine2Point = line2.Point1.Row < line2.Point2.Row? line2.Point1 : line2.Point2;

                var lowestOfTheHighs = highestLine1Point.Row < highestLine2Point.Row ? highestLine1Point : highestLine2Point;
                var highestOfTheLows = lowestLine1Point.Row > lowestLine2Point.Row ? lowestLine1Point : lowestLine2Point;

                return [lowestOfTheHighs, highestOfTheLows];
            }

            var rightestLine1Point = line1.Point1.Col < line1.Point2.Col? line1.Point2 : line1.Point1;
            var leftestLine1Point = line1.Point1.Col < line1.Point2.Col? line1.Point1 : line1.Point2;

            var rightestLine2Point = line2.Point1.Col < line2.Point2.Col? line2.Point2 : line2.Point1;
            var leftestLine2Point = line2.Point1.Col < line2.Point2.Col? line2.Point1 : line2.Point2;

            var leftestOfTheRights = rightestLine1Point.Col < rightestLine2Point.Col ? rightestLine1Point : rightestLine2Point;
            var rightestOfTheLefts = leftestLine1Point.Col > leftestLine2Point.Col ? leftestLine1Point : leftestLine2Point;

            return [leftestOfTheRights, rightestOfTheLefts];
        }
    }

    public double YIntercept()
    {
        if (double.IsPositiveInfinity(Slope))
        {
            return double.NaN;
        }

        return Point1.Row - Slope * Point1.Col;
    }

    public double XIntercept()
    {
        if (Slope == 0)
        {
            return double.NaN;
        }

        if (double.IsPositiveInfinity(Slope))
        {
            return Point1.Col;
        }

        return Point1.Col - Point1.Row / Slope;
    }

    public double GetRowGivenCol(int col)
    {
        if (double.IsPositiveInfinity(Slope))
        {
            return double.NaN;
        }

        // y = mx + c
        return Slope * col + YIntercept();
    }

    public double GetColGivenRow(int row)
    {
        if (double.IsPositiveInfinity(Slope))
        {
            return Point1.Col;
        }

        if (Slope == 0)
        {
            return double.NaN;
        }

        // y = mx + c
        // mx = y - c
        // x = (y - c) / m
        return (row - YIntercept()) / Slope;
    }

    private double CalculateSlope()
    {
        var run = Point2.Col - Point1.Col;

        if (run == 0)
        {
            return double.PositiveInfinity;
        }

        var rise = Point2.Row - Point1.Row;

        return rise / (double)run;
    }
}
