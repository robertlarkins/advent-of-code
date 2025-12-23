using Larkins.AdventOfCode.Models;

namespace Larkins.AdventOfCode.AdventOfCode2025.Day09MovieTheatre;

public class Year2025Day09Part02Solver
{
    private readonly SortedList<int, SortedSet<(int LineStartRow, int LineEndRow)>> verticalLines = new();
    private readonly SortedList<int, SortedSet<(int LineStartCol, int LineEndCol)>> horizontalLines = new();
    private List<GridPoint> gridPoints = [];

    public Year2025Day09Part02Solver(string input)
    {
        ParseInput(input);
    }

    public long Solve()
    {
        var largestArea = 0L;

        for (var gp1 = 0; gp1 < gridPoints.Count - 1; gp1++)
        {
            for (var gp2 = 1; gp2 < gridPoints.Count; gp2++)
            {
                var p1 = gridPoints[gp1];
                var p2 = new GridPoint(gridPoints[gp1].Row, gridPoints[gp2].Col);
                var p3 = gridPoints[gp2];
                var p4 = new GridPoint(gridPoints[gp2].Row, gridPoints[gp1].Col);

                var line1 = new Line(p1, p2);
                var line2 = new Line(p2, p3);
                var line3 = new Line(p3, p4);
                var line4 = new Line(p4, p1);

                if (IsLineValidForRectangleEdge(line1) &&
                    IsLineValidForRectangleEdge(line2) &&
                    IsLineValidForRectangleEdge(line3) &&
                    IsLineValidForRectangleEdge(line4))
                {
                    largestArea = Math.Max(largestArea, CalculateRectangularArea(gridPoints[gp1], gridPoints[gp2]));
                }
            }
        }

        return largestArea;

        static long CalculateRectangularArea(GridPoint point1, GridPoint point2)
        {
            // the 1L makes these longs, otherwise height and width are int, which multiplied
            // together give an int. This product int is then placed into a long.
            var width = Math.Abs(point1.Col - point2.Col) + 1L;
            var height = Math.Abs(point1.Row - point2.Row) + 1L;

            return height * width;
        }
    }

    private bool IsLineValidForRectangleEdge(Line line)
    {
        // is line horizontal or vertical?
        if (double.IsPositiveInfinity(line.Slope)) // vertical
        {
            var startRow = Math.Min(line.Point1.Row, line.Point2.Row);
            var endRow = Math.Max(line.Point1.Row, line.Point2.Row);
            var lineColumn = line.Point1.Col;

            var possibleHorizontalLines = horizontalLines.Where(x => startRow <= x.Key && x.Key <= endRow);

            foreach (var horizontalLineRow in possibleHorizontalLines)
            {
                foreach (var horizontalLine in horizontalLineRow.Value)
                {
                    if (horizontalLine.LineStartCol <= lineColumn &&
                        lineColumn <= horizontalLine.LineEndCol)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        var startCol = Math.Min(line.Point1.Col, line.Point2.Col);
        var endCol = Math.Max(line.Point1.Col, line.Point2.Col);
        var lineRow = line.Point1.Row;

        var possibleVerticalLines = verticalLines.Where(x => startCol <= x.Key && x.Key <= endCol);

        foreach (var verticalLineRow in possibleVerticalLines)
        {
            foreach (var verticalLine in verticalLineRow.Value)
            {
                if (verticalLine.LineStartRow <= lineRow &&
                    lineRow <= verticalLine.LineEndRow)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private void ParseInput(string input)
    {
        // Find the highest line. The outside will be above this. Work around clock wise defining
        // the boundary. The points of this boundary can be used with what I've been trying to do.
        var (parsedGridPoints, highestPointIndex) = ParseInputIntoPoints(input);
        this.gridPoints = parsedGridPoints;

        // go through list of points starting at highestPointIndex. The highestPointIndex is the
        // left point in the horizontal line found on the lowest row. If there are multiple lines on
        // this row, then the first one found is used.
        var boundaryPoints = FormBoundaryPoints(gridPoints, highestPointIndex);
        ConvertPointsToLines(boundaryPoints);
    }

    private void ConvertPointsToLines(List<GridPoint> points)
    {
        for (var i = 0; i < points.Count ; i++)
        {
            var thisPoint = points[i];
            var nextPoint = points[(i + 1) % points.Count];

            if (thisPoint.Col == nextPoint.Col)
            {
                var verticalLineStart = Math.Min(thisPoint.Row, nextPoint.Row);
                var verticalLineEnd = Math.Max(thisPoint.Row, nextPoint.Row);

                var verticalLine = (verticalLineStart, verticalLineEnd);

                if (!verticalLines.TryAdd(thisPoint.Col, [verticalLine]))
                {
                    verticalLines[thisPoint.Col].Add(verticalLine);
                }
            }
            else
            {
                var horizontalLineStart = Math.Min(thisPoint.Col, nextPoint.Col);
                var horizontalLineEnd = Math.Max(thisPoint.Col, nextPoint.Col);

                var horizontalLine = (horizontalLineStart, horizontalLineEnd);

                if (!horizontalLines.TryAdd(thisPoint.Row, [horizontalLine]))
                {
                    horizontalLines[thisPoint.Row].Add(horizontalLine);
                }
            }
        }
    }

    private static List<GridPoint> FormBoundaryPoints(List<GridPoint> gridPoints, int highestPointIndex)
    {
        var highestPoint = gridPoints[highestPointIndex];
        List<GridPoint> boundaryPoints = [new(highestPoint.Row - 1, highestPoint.Col - 1)];

        for (var i = 1; i < gridPoints.Count; i++)
        {
            var prevPoint = gridPoints[(highestPointIndex + i - 1) % gridPoints.Count];
            var thisPoint = gridPoints[(highestPointIndex + i) % gridPoints.Count];
            var nextPoint = gridPoints[(highestPointIndex + i + 1) % gridPoints.Count];

            var prevBoundaryPoint = boundaryPoints[i - 1];

            // is the line horizontal
            if (prevPoint.Row == thisPoint.Row)
            {
                var isBoundaryAbove = prevBoundaryPoint.Row < thisPoint.Row;
                var isRightGoingLine = prevPoint.Col < thisPoint.Col;
                var isNextLineDown = nextPoint.Row > thisPoint.Row;

                var thisPointOffset = isBoundaryAbove == isNextLineDown ? 1 : -1;
                if (!isRightGoingLine)
                {
                    thisPointOffset *= -1;
                }

                var nextBoundaryPoint = prevBoundaryPoint with
                {
                    Col = thisPoint.Col + thisPointOffset,
                };
                boundaryPoints.Add(nextBoundaryPoint);
            }
            else
            {
                var isBoundaryLeft = prevBoundaryPoint.Col < thisPoint.Col;
                var isNextLineLeft = nextPoint.Col < thisPoint.Col;
                var isDownGoingLine = prevPoint.Row < thisPoint.Row;

                var thisPointOffset = isBoundaryLeft == isNextLineLeft ? -1 : 1;
                if (!isDownGoingLine)
                {
                    thisPointOffset *= -1;
                }

                var nextBoundaryPoint = prevBoundaryPoint with
                {
                    Row = thisPoint.Row + thisPointOffset
                };
                boundaryPoints.Add(nextBoundaryPoint);
            }
        }

        return boundaryPoints;
    }

    private (List<GridPoint>, int highestPointIndex) ParseInputIntoPoints(string input)
    {
        List<GridPoint> pointsInOrder = [];
        var inputSpan = input.AsSpan();
        var lines = inputSpan.Split(Environment.NewLine);
        var highestPointIndex = 0;
        var lowestRow = int.MaxValue;
        var index = 0;

        foreach (var lineRange in lines)
        {
            var line = inputSpan[lineRange];

            var parts = line.Split(",");

            List<int> coords = [];

            foreach (var partRange in parts)
            {
                coords.Add(int.Parse(line[partRange]));
            }

            if (coords[1] < lowestRow)
            {
                lowestRow = coords[1];
                highestPointIndex = index;
            }

            lowestRow = Math.Min(lowestRow, coords[1]);

            pointsInOrder.Add(new GridPoint(coords[1], coords[0]));

            index++;
        }

        // is index at start of row line
        if (pointsInOrder[highestPointIndex].Row !=
            pointsInOrder[(highestPointIndex + 1) % pointsInOrder.Count].Row)
        {
            highestPointIndex = (highestPointIndex - 1 + pointsInOrder.Count) % pointsInOrder.Count;
        }

        return (pointsInOrder, highestPointIndex);
    }
}
