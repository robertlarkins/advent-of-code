using System.Text;
using Larkins.AdventOfCode.Models;

namespace Larkins.AdventOfCode.AdventOfCode2024.Day15WarehouseWoes;

public class Year2024Day15Part01Solver
{
    private Warehouse warehouse;
    private readonly List<Direction> directions = [];

    public Year2024Day15Part01Solver(IEnumerable<string> input)
    {
        var inputData = input.ToList();
        ParseInput(inputData);
    }

    public int Solve()
    {
        warehouse.PrintGrid();

        foreach (var direction in directions)
        {
            Console.WriteLine(direction);
            warehouse.UpdateWarehouse(direction);
            warehouse.PrintGrid();
        }

        return warehouse.CalculateGpsSum();
    }

    private void ParseInput(List<string> input)
    {
        var warehouseLines = new List<string>();
        var directionLine = new StringBuilder();

        var isWarehouse = true;

        foreach (var line in input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                isWarehouse = false;
                continue;
            }

            if (isWarehouse)
            {
                warehouseLines.Add(line);
            }
            else
            {
                directionLine.Append(line);
            }
        }

        warehouse = Warehouse.Create(warehouseLines);

        for (var i = 0; i < directionLine.Length; i++)
        {
            var direction = directionLine[i] switch
            {
                '^' => Direction.Up,
                'v' => Direction.Down,
                '<' => Direction.Left,
                '>' => Direction.Right,
                _ => throw new ArgumentException($"Invalid direction character: {directionLine[i]}")
            };

            directions.Add(direction);
        }
    }

    private class Warehouse
    {
        private const char RobotChar = '@';
        private const char WallChar = '#';
        private const char BoxChar = 'O';
        private const char EmptySpaceChar = '.';

        private readonly char[,] grid;

        private Warehouse(
            int height,
            int width,
            char[,] grid,
            GridPoint robotPosition)
        {
            Height = height;
            Width = width;
            RobotPosition = robotPosition;
            this.grid = grid;
        }

        public int Height { get; set; }
        public int Width { get; set; }
        public GridPoint RobotPosition { get; set; }

        public int CalculateGpsSum()
        {
            var gpsPositions = new List<int>();

            for (var row = 1; row < Height - 1; row++)
            {
                for (var col = 1; col < Width - 1; col++)
                {
                    if (grid[row, col] == BoxChar)
                    {
                        gpsPositions.Add(row * 100 + col);
                    }
                }
            }

            return gpsPositions.Sum();
        }

        public void UpdateWarehouse(Direction direction)
        {
            if (direction == Direction.Up)
            {
                if (CanPushUp())
                {
                    PushUp();
                }
            }
            else if (direction == Direction.Down)
            {
                if (CanPushDown())
                {
                    PushDown();
                }
            }
            else if (direction == Direction.Left)
            {
                if (CanPushLeft())
                {
                    PushLeft();
                }
            }
            else if (direction == Direction.Right)
            {
                if (CanPushRight())
                {
                    PushRight();
                }
            }
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

        private bool CanPushLeft()
        {
            for (var col = RobotPosition.Col - 1; col >= 0; col--)
            {
                var element = grid[RobotPosition.Row, col];

                switch (element)
                {
                    case EmptySpaceChar: return true;
                    case WallChar: return false;
                    case BoxChar: continue;
                }
            }

            throw new UnreachableException();
        }

        private bool CanPushUp()
        {
            for (var row = RobotPosition.Row - 1; row >= 0; row--)
            {
                var element = grid[row, RobotPosition.Col];

                switch (element)
                {
                    case EmptySpaceChar: return true;
                    case WallChar: return false;
                    case BoxChar: continue;
                }
            }

            throw new UnreachableException();
        }

        private bool CanPushDown()
        {
            for (var row = RobotPosition.Row + 1; row < Height; row++)
            {
                var element = grid[row, RobotPosition.Col];

                switch (element)
                {
                    case EmptySpaceChar: return true;
                    case WallChar: return false;
                    case BoxChar: continue;
                }
            }

            throw new UnreachableException();
        }

        private void PushUp()
        {
            var position = RobotPosition;
            var itemToMoveChar = grid[position.Row, position.Col];

            position = position with { Row = position.Row - 1 };

            while (grid[position.Row, position.Col] != EmptySpaceChar)
            {
                (grid[position.Row, position.Col], itemToMoveChar) =
                    (itemToMoveChar, grid[position.Row, position.Col]);

                position = position with { Row = position.Row - 1 };
            }

            grid[position.Row, position.Col] = itemToMoveChar;
            grid[RobotPosition.Row, RobotPosition.Col] = EmptySpaceChar;
            RobotPosition = RobotPosition with { Row = RobotPosition.Row - 1 };
        }


        private void PushDown()
        {
            var position = RobotPosition;
            var itemToMoveChar = grid[position.Row, position.Col];

            position = position with { Row = position.Row + 1 };

            while (grid[position.Row, position.Col] != EmptySpaceChar)
            {
                (grid[position.Row, position.Col], itemToMoveChar) =
                    (itemToMoveChar, grid[position.Row, position.Col]);

                position = position with { Row = position.Row + 1 };
            }

            grid[position.Row, position.Col] = itemToMoveChar;
            grid[RobotPosition.Row, RobotPosition.Col] = EmptySpaceChar;
            RobotPosition = RobotPosition with { Row = RobotPosition.Row + 1 };
        }

        private void PushLeft()
        {
            var position = RobotPosition;
            var itemToMoveChar = grid[position.Row, position.Col];

            position = position with { Col = position.Col - 1 };

            while (grid[position.Row, position.Col] != EmptySpaceChar)
            {
                (grid[position.Row, position.Col], itemToMoveChar) =
                    (itemToMoveChar, grid[position.Row, position.Col]);

                position = position with { Col = position.Col - 1 };
            }

            grid[position.Row, position.Col] = itemToMoveChar;
            grid[RobotPosition.Row, RobotPosition.Col] = EmptySpaceChar;
            RobotPosition = RobotPosition with { Col = RobotPosition.Col - 1 };
        }

        private bool CanPushRight()
        {
            for (var col = RobotPosition.Col + 1; col < Width; col++)
            {
                var element = grid[RobotPosition.Row, col];

                switch (element)
                {
                    case EmptySpaceChar: return true;
                    case WallChar: return false;
                    case BoxChar: continue;
                }
            }

            throw new UnreachableException();
        }

        private void PushRight()
        {
            var position = RobotPosition;
            var itemToMoveChar = grid[position.Row, position.Col];

            position = position with { Col = position.Col + 1 };

            while (grid[position.Row, position.Col] != EmptySpaceChar)
            {
                (grid[position.Row, position.Col], itemToMoveChar) =
                    (itemToMoveChar, grid[position.Row, position.Col]);

                position = position with { Col = position.Col + 1 };
            }

            grid[position.Row, position.Col] = itemToMoveChar;
            grid[RobotPosition.Row, RobotPosition.Col] = EmptySpaceChar;
            RobotPosition = RobotPosition with { Col = RobotPosition.Col + 1 };
        }

        public static Warehouse Create(List<string> warehouseInput)
        {
            var height = warehouseInput.Count;
            var width = warehouseInput.First().Length;

            var grid = new char[height, width];
            GridPoint robotPosition = new GridPoint(0, 0);

            for (var row = 0; row < height; row++)
            {
                for (var col = 0; col < width; col++)
                {
                    if (warehouseInput[row][col] == RobotChar)
                    {
                        robotPosition = new GridPoint(row, col);
                    }
                    grid[row, col] = warehouseInput[row][col];
                }
            }

            return new Warehouse(height, width, grid, robotPosition);
        }
    };
}
