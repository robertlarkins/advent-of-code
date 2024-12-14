namespace Larkins.AdventOfCode.AdventOfCode2024.Day14RestroomRedoubt;

public class Year2024Day14Part02Solver
{
    private readonly int gridHeight;
    private readonly int gridWidth;
    private readonly int seconds;
    private readonly List<Robot> robots;

    public Year2024Day14Part02Solver(
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
        var locations = robots;
        (long minDistance, int second) best = (long.MaxValue, 0);

        for (var second = 1; second < seconds; second++)
        {
            for (var i = 0; i < locations.Count; i++)
            {
                var newLocation = CalculateRobotLocation(locations[i]);
                locations[i] = newLocation;
            }

            var com = CalculateCentreOfMass(locations);
            var meanDistance = MeanDistance(locations, com);

            if (meanDistance >= best.minDistance)
            {
                continue;
            }

            best = (meanDistance, second);

            PrintGrid(locations);
            Console.WriteLine($"Distance: {meanDistance}");
            Console.WriteLine($"Second: {second}");
        }

        return best.second;
    }

    private long MeanDistance(List<Robot> locations, (int row, int col) centreOfMass)
    {
        var meanDistance = 0L;
        foreach (var robot in locations)
        {
            var rowDiff = (long)Math.Pow(centreOfMass.row - robot.Position.Row, 2);
            var colDiff = (long)Math.Pow(centreOfMass.col - robot.Position.Col, 2);

            meanDistance += rowDiff + colDiff;
        }

        return meanDistance;
    }

    private (int ComRow, int ComCol) CalculateCentreOfMass(List<Robot> locations)
    {
        var comX = 0;
        var comY = 0;

        foreach (var robot in locations)
        {
            comX += robot.Position.Col;
            comY += robot.Position.Row;
        }

        double count = locations.Count;

        return ((int)Math.Round(comY / count), (int)Math.Round(comX / count));
    }

    private void PrintGrid(List<Robot> robots)
    {
        var grid = new char[gridHeight, gridWidth];

        foreach (var robot in robots)
        {
            grid[robot.Position.Row, robot.Position.Col] = '#';
        }

        for (var row = 0; row < gridHeight; row++)
        {
            for (var col = 12; col < gridWidth-12; col++)
            {
                Console.Write(grid[row, col] == '\0' ? '.' : grid[row, col]);
            }
            Console.WriteLine();
        }
    }

    private Robot CalculateRobotLocation(Robot robot)
    {
        var colLocation = robot.Position.Col + robot.Velocity.Col;
        var rowLocation = robot.Position.Row + robot.Velocity.Row;

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

        return robot with { Position = (rowLocation, colLocation) };
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

    private Robot ParseRobotInfo(string robotInfo)
    {
        var parts = robotInfo.Split(" ");
        var positionParts = parts[0][2..].Split(",").Select(int.Parse).ToArray();
        var velocityParts = parts[1][2..].Split(",").Select(int.Parse).ToArray();

        var position = (positionParts[1], positionParts[0]);
        var velocity = (velocityParts[1], velocityParts[0]);

        return new Robot(position, velocity);
    }

    public record Robot(
        (int Row, int Col) Position,
        (int Row, int Col) Velocity);
}
