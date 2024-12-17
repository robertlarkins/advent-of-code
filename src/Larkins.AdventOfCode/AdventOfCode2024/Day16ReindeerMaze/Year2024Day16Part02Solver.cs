using Larkins.AdventOfCode.Models;

namespace Larkins.AdventOfCode.AdventOfCode2024.Day16ReindeerMaze;

public class Year2024Day16Part02Solver
{
    private Maze maze;
    private readonly List<Direction> directions = [];
    private Dictionary<(int row, int col, Direction direction), int> bestScore = [];

    public Year2024Day16Part02Solver(IEnumerable<string> input)
    {
        var inputData = input.ToList();
        ParseInput(inputData);
    }

    public int Solve()
    {
        var minScore = TraverseMaze();

        return BackTrackMaze(minScore);
    }

    private int BackTrackMaze(int minScore)
    {
        var seenPlaces = new HashSet<(int row, int col)>();

        // starting at end, find the neighbours that have the smallest scores
        // so end - 1 - rotations * 1000
        var queue = new Queue<(int row, int col, int scoreToHere, Direction directionIn)>();

        var endpointInfo = bestScore
            .Where(kvp => kvp.Key.row == maze.EndPoint.Row && kvp.Key.col == maze.EndPoint.Col && kvp.Value == minScore);

        foreach (var kvp in endpointInfo)
        {
            queue.Enqueue((kvp.Key.row, kvp.Key.col, kvp.Value, kvp.Key.direction));
        }



        while (queue.Count > 0)
        {
            // dequeue, get neighbours that have a score that matches the previous step from
            // get neighbours in the opposite direction to the directionIn
            var currentPlace = queue.Dequeue();
            seenPlaces.Add((currentPlace.row, currentPlace.col));

            var reverseNeighbours = GetReverseNeighbours(
                currentPlace.row,
                currentPlace.col,
                currentPlace.directionIn);

            foreach (var neighbour in reverseNeighbours)
            {
                queue.Enqueue(neighbour);
            }
        }

        return seenPlaces.Count;
    }

    private List<(int row, int col, int scoreToHere, Direction direction)> GetReverseNeighbours(int row, int col, Direction directionIn)
    {
        var (nRow, nCol) = directionIn switch
        {
            Direction.Up => (row + 1, col),
            Direction.Down => (row - 1, col),
            Direction.Left => (row, col + 1),
            Direction.Right => (row, col - 1),
            _ => throw new UnreachableException()
        };

        var neighbours = bestScore
            .Where(kvp => kvp.Key.row == nRow && kvp.Key.col == nCol)
            .ToList();

        if (neighbours.Count == 0)
        {
            return [];
        }

        var updatedNeighbours = new List<(int row, int col, int scoreToHere, Direction directionIn)>();

        // subtract turns from direction in.
        foreach (var neighbour in neighbours)
        {
            var turns = directionIn.TurnsToDirection(neighbour.Key.direction);

            updatedNeighbours.Add((neighbour.Key.row, neighbour.Key.col, neighbour.Value + turns * 1000, neighbour.Key.direction));
        }

        var smallestNeighbourScore = updatedNeighbours.Min(info => info.scoreToHere);

        var reverseNeighbours = updatedNeighbours.Where(info => info.scoreToHere == smallestNeighbourScore).ToList();

        return reverseNeighbours;
    }

    private int TraverseMaze()
    {
        var queue = new Queue<(int row, int col, int scoreToHere, Direction directionIn)>();
        queue.Enqueue((maze.StartPoint.Row, maze.StartPoint.Col, 0, Direction.Right));

        while (queue.Count > 0)
        {
            var currentPlace = queue.Dequeue();

            var key = (currentPlace.row, currentPlace.col, currentPlace.directionIn);

            // update best score
            if (!bestScore.TryAdd(key, currentPlace.scoreToHere))
            {
                if (bestScore[key] < currentPlace.scoreToHere)
                {
                    continue;
                }

                bestScore[key] = currentPlace.scoreToHere;
            }

            // add neighbours
            var pathNeighbours = maze.GetPathNeighbours(currentPlace.row, currentPlace.col);

            foreach (var neighbour in pathNeighbours)
            {
                // cost to get to this neighbour
                var turns = currentPlace.directionIn.TurnsToDirection(neighbour.direction);
                var costToHere = currentPlace.scoreToHere + 1 + turns * 1000;
                // add neighbour to queue
                queue.Enqueue((neighbour.neighbourRow, neighbour.neighbourCol, costToHere, neighbour.direction));
            }
        }

        var finalScore = bestScore
            .Where(kvp => kvp.Key.row == maze.EndPoint.Row && kvp.Key.col == maze.EndPoint.Col)
            .Min(x => x.Value);

        return finalScore;
    }

    private void ParseInput(List<string> input)
    {
        maze = Maze.Create(input);
    }

    private record Reindeer(int Row, int Col, Direction Direction);

    private class Maze
    {
        private const char StartChar = 'S';
        private const char EndChar = 'E';
        private const char WallChar = '#';
        private const char EmptySpaceChar = '.';

        private record Cell(
            int Row,
            int Col,
            char Type,
            int Score);

        private readonly Cell[,] grid;

        private Maze(
            int height,
            int width,
            Cell[,] grid,
            GridPoint startPoint,
            GridPoint endPoint)
        {
            Height = height;
            Width = width;
            this.grid = grid;
            StartPoint = startPoint;
            EndPoint = endPoint;
        }

        public int Height { get; set; }
        public int Width { get; set; }

        public GridPoint StartPoint { get; }

        public GridPoint EndPoint { get; }


        public List<(int neighbourRow, int neighbourCol, Direction direction)> GetPathNeighbours(int row, int col)
        {
            var neighbours = new[] {
                (row - 1, col, Direction.Up),
                (row + 1, col, Direction.Down),
                (row, col - 1, Direction.Left),
                (row, col + 1, Direction.Right),
            };

            var allowableNeighbours = new List<(int neighbourRow, int neighbourCol, Direction direction)>();

            foreach ((int neighbourRow, int neighbourCol, Direction direction) in neighbours)
            {
                if (grid[neighbourRow, neighbourCol].Type == WallChar)
                {
                    continue;
                }

                allowableNeighbours.Add((neighbourRow, neighbourCol, direction));
            }

            return allowableNeighbours;
        }

        public void PrintGrid()
        {
            for (var row = 0; row < Height; row++)
            {
                for (var col = 0; col < Width; col++)
                {
                    Console.Write(grid[row, col]);
                }
                Console.WriteLine();
            }
        }

        public static Maze Create(List<string> input)
        {
            var height = input.Count;
            var width = input.First().Length;

            var grid = new Cell[height, width];
            var startPoint = new GridPoint(0, 0);
            var endPoint = new GridPoint(0, 0);

            for (var row = 0; row < height; row++)
            {
                for (var col = 0; col < width; col++)
                {
                    if (input[row][col] == StartChar)
                    {
                        startPoint = new GridPoint(row, col);
                    }
                    else if (input[row][col] == EndChar)
                    {
                        endPoint = new GridPoint(row, col);
                    }

                    grid[row, col] = new Cell(row, col, input[row][col], int.MaxValue);
                }
            }

            return new Maze(
                height,
                width,
                grid,
                startPoint,
                endPoint);
        }
    };
}
