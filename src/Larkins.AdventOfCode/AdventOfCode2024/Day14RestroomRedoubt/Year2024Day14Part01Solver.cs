namespace Larkins.AdventOfCode.AdventOfCode2024.Day14RestroomRedoubt;

public class Year2024Day14Part01Solver
{
    private readonly int gridHeight;
    private readonly int gridWidth;
    private readonly int seconds;
    private List<Robot> robots;

    public Year2024Day14Part01Solver(
        IEnumerable<string> input,
        int gridHeight,
        int gridWidth,
        int seconds)
    {
        this.gridHeight = gridHeight;
        this.gridWidth = gridWidth;
        this.seconds = seconds;

        var inputData = input.ToList();
        robots = ParseInput(inputData);
    }

    public int Solve()
    {
        var newLocations = new List<(int newRow, int newCol)>();

        foreach (var robot in robots)
        {
            var newLocation = CalculateRobotLocation(robot, seconds);
            newLocations.Add(newLocation);
        }

        var quadWidth = gridWidth / 2;
        var quadHeight = gridHeight / 2;

        var quad1Boundaries = new QuadBoundary(0, quadWidth - 1, 0, quadHeight - 1); // top left
        var quad2Boundaries = new QuadBoundary(gridWidth - quadWidth, gridWidth - 1, 0, quadHeight - 1); // top right
        var quad3Boundaries = new QuadBoundary(0, quadWidth - 1, gridHeight - quadHeight, gridHeight - 1); // bottom left
        var quad4Boundaries = new QuadBoundary(gridWidth - quadWidth, gridWidth - 1, gridHeight - quadHeight, gridHeight - 1); // bottom right

        var quad1 = newLocations.Count(quad1Boundaries.IsInBounds);
        var quad2 = newLocations.Count(quad2Boundaries.IsInBounds);
        var quad3 = newLocations.Count(quad3Boundaries.IsInBounds);
        var quad4 = newLocations.Count(quad4Boundaries.IsInBounds);

        return quad1 * quad2 * quad3 * quad4;
    }

    public (int newRow, int newCol) CalculateRobotLocation(Robot robot, int seconds)
    {
        var colLocation = robot.StartPosition.Col + robot.Velocity.Col * seconds;
        var rowLocation = robot.StartPosition.Row + robot.Velocity.Row * seconds;

        colLocation %= gridWidth;
        rowLocation %= gridHeight;

        if (rowLocation < 0)
        {
            rowLocation = gridHeight + rowLocation;
        }

        if (colLocation < 0)
        {
            colLocation = gridWidth + colLocation;
        }

        return (rowLocation, colLocation);
    }

    private List<Robot> ParseInput(List<string> input)
    {
        var robots = new List<Robot>();

        foreach (var line in input)
        {
            var robot = ParseRobotInfo(line);
            robots.Add(robot);
        }

        return robots;
    }

    public Robot ParseRobotInfo(string robotInfo)
    {
        var parts = robotInfo.Split(" ");
        var positionParts = parts[0][2..].Split(",").Select(int.Parse).ToArray();
        var velocityParts = parts[1][2..].Split(",").Select(int.Parse).ToArray();

        var position = (positionParts[1], positionParts[0]);
        var velocity = (velocityParts[1], velocityParts[0]);

        return new Robot(position, velocity);
    }

    public record Robot(
        (int Row, int Col) StartPosition,
        (int Row, int Col) Velocity);

    private record QuadBoundary(
        int Left,
        int Right,
        int Top,
        int Bottom)
    {
        public bool IsInBounds((int row, int col) pos)
        {
            var isRowInBounds = pos.row >= Top && pos.row <= Bottom;
            var isColInBounds = pos.col >= Left && pos.col <= Right;

            return isRowInBounds && isColInBounds;
        }
    };
}
