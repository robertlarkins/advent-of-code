using System.Text;
using Larkins.AdventOfCode.Models;

namespace Larkins.AdventOfCode.AdventOfCode2024.Day15WarehouseWoes;

public class Year2024Day15Part02Solver
{
    private Warehouse warehouse;
    private readonly List<Direction> directions = [];

    public Year2024Day15Part02Solver(IEnumerable<string> input)
    {
        var inputData = input.ToList();
        ParseInput(inputData);
    }

    public int Solve()
    {
        // warehouse.PrintGrid();
        var count = 0;

        foreach (var direction in directions)
        {
            if (count % 10 == 0)
            {
                Console.WriteLine($"Count {count}. Direction: {direction}");
                // Console.WriteLine($"Direction: {direction}");
            }

            warehouse.UpdateWarehouse(direction);
            // warehouse.PrintGrid();
            count++;
        }

        // warehouse.PrintGrid();

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
        private const char LeftBoxChar = '[';
        private const char RightBoxChar = ']';
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
                    if (grid[row, col] == LeftBoxChar)
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
                    case RightBoxChar:
                    case LeftBoxChar:
                        continue;
                }
            }

            throw new UnreachableException();
        }

        private bool CanPushUp()
        {
            var cellsToCheck = new Queue<GridPoint>();
            cellsToCheck.Enqueue(RobotPosition with { Row = RobotPosition.Row - 1 });

            // seenCells is need as a box half will try enqueueing the other half, even if the other
            // half has already been done.
            var seenCells = new HashSet<GridPoint>();

            while (cellsToCheck.Count > 0)
            {
                var gridPoint = cellsToCheck.Dequeue();
                if (!seenCells.Add(gridPoint))
                {
                    continue;
                }

                var (row, col) = gridPoint;

                if (grid[row, col] == EmptySpaceChar)
                {
                    continue;
                }

                if (grid[row, col] == WallChar)
                {
                    return false;
                }

                if (grid[row, col] == LeftBoxChar)
                {
                    cellsToCheck.Enqueue(new GridPoint(row, col + 1));
                    cellsToCheck.Enqueue(new GridPoint(row - 1, col));
                }

                if (grid[row, col] == RightBoxChar)
                {
                    cellsToCheck.Enqueue(new GridPoint(row, col - 1));
                    cellsToCheck.Enqueue(new GridPoint(row - 1, col));
                }
            }

            return true;
        }

        private bool CanPushDown()
        {
            var cellsToCheck = new Queue<GridPoint>();
            cellsToCheck.Enqueue(RobotPosition with { Row = RobotPosition.Row + 1 });

            // seenCells is need as a box half will try enqueueing the other half, even if the other
            // half has already been done.
            var seenCells = new HashSet<GridPoint>();

            while (cellsToCheck.Count > 0)
            {
                var gridPoint = cellsToCheck.Dequeue();
                if (!seenCells.Add(gridPoint))
                {
                    continue;
                }

                var (row, col) = gridPoint;

                if (grid[row, col] == EmptySpaceChar)
                {
                    continue;
                }

                if (grid[row, col] == WallChar)
                {
                    return false;
                }

                // this cell will be part of a box, so enqueue.
                cellsToCheck.Enqueue(new GridPoint(row + 1, col));

                // grid[row, col] will only be left or right of box.
                var addLeftOrRight = grid[row, col] == LeftBoxChar ? 1 : -1;

                cellsToCheck.Enqueue(new GridPoint(row, col + addLeftOrRight));
            }

            return true;
        }

        private void PushUp()
        {
            var cellsToCheck = new Queue<GridPoint>();
            cellsToCheck.Enqueue(RobotPosition);

            // seenCells is need as a box half will try enqueueing the other half, even if the other
            // half has already been done.
            var seenCells = new HashSet<GridPoint>();

            while (cellsToCheck.Count > 0)
            {
                var gridPoint = cellsToCheck.Dequeue();
                var (row, col) = gridPoint;

                if (grid[row, col] == EmptySpaceChar)
                {
                    continue;
                }

                if (grid[row, col] == RobotChar)
                {
                    cellsToCheck.Enqueue(new GridPoint(row - 1, col));
                    seenCells.Add(gridPoint);
                    continue;
                }

                if (!seenCells.Add(gridPoint))
                {
                    continue;
                }

                // this cell will be part of a box, so enqueue.
                cellsToCheck.Enqueue(new GridPoint(row - 1, col));

                // grid[row, col] will only be left or right of box.
                var addLeftOrRight = grid[row, col] == LeftBoxChar ? 1 : -1;

                cellsToCheck.Enqueue(new GridPoint(row, col + addLeftOrRight));
            }

            var ascendingComparer = Comparer<GridPoint>.Create(((a, b) =>
            {
                var rowComparison = a.Row.CompareTo(b.Row);

                if (rowComparison != 0)
                {
                    return rowComparison;
                }

                return a.Col.CompareTo(b.Col);
            }));
            var cellsToMove = new SortedSet<GridPoint>(ascendingComparer);

            foreach (var cell in seenCells)
            {
                cellsToMove.Add(cell);
            }

            // Shift each cell up
            foreach (var cell in cellsToMove)
            {
                (grid[cell.Row - 1, cell.Col], grid[cell.Row, cell.Col]) = (grid[cell.Row, cell.Col], grid[cell.Row - 1, cell.Col]);
            }

            RobotPosition = RobotPosition with { Row = RobotPosition.Row - 1 };
        }

        private void PushDown()
        {
            var cellsToCheck = new Queue<GridPoint>();
            cellsToCheck.Enqueue(RobotPosition);

            // seenCells is need as a box half will try enqueueing the other half, even if the other
            // half has already been done.
            var seenCells = new HashSet<GridPoint>();

            while (cellsToCheck.Count > 0)
            {
                var gridPoint = cellsToCheck.Dequeue();
                var (row, col) = gridPoint;

                if (grid[row, col] == EmptySpaceChar)
                {
                    continue;
                }

                if (grid[row, col] == RobotChar)
                {
                    cellsToCheck.Enqueue(new GridPoint(row + 1, col));
                    seenCells.Add(gridPoint);
                    continue;
                }

                if (!seenCells.Add(gridPoint))
                {
                    continue;
                }

                // this cell will be part of a box, so enqueue.
                cellsToCheck.Enqueue(new GridPoint(row + 1, col));

                // grid[row, col] will only be left or right of box.
                var addLeftOrRight = grid[row, col] == LeftBoxChar ? 1 : -1;

                cellsToCheck.Enqueue(new GridPoint(row, col + addLeftOrRight));
            }

            var descendingComparer = Comparer<GridPoint>.Create((a, b) =>
            {
                var rowComparison = b.Row.CompareTo(a.Row);

                if (rowComparison != 0)
                {
                    return rowComparison;
                }

                return a.Col.CompareTo(b.Col);
            });


            var cellsToMove = new SortedSet<GridPoint>(descendingComparer);

            foreach (var cell in seenCells)
            {
                cellsToMove.Add(cell);
            }

            // Shift each cell up
            foreach (var cell in cellsToMove)
            {
                (grid[cell.Row + 1, cell.Col], grid[cell.Row, cell.Col]) = (grid[cell.Row, cell.Col], grid[cell.Row + 1, cell.Col]);
            }

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
                    case LeftBoxChar:
                    case RightBoxChar:
                        continue;
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

            var grid = new char[height, width * 2];
            GridPoint robotPosition = new GridPoint(0, 0);

            for (var row = 0; row < height; row++)
            {
                for (var col = 0; col < width; col++)
                {
                    var gridCol = col * 2;
                    var inputCell = warehouseInput[row][col];

                    if (warehouseInput[row][col] == RobotChar)
                    {
                        robotPosition = new GridPoint(row, gridCol);
                    }

                    grid[row, gridCol] = inputCell switch
                    {
                        EmptySpaceChar => EmptySpaceChar,
                        WallChar => WallChar,
                        BoxChar => LeftBoxChar,
                        RobotChar => RobotChar,
                        _ => throw new ArgumentException(),
                    };

                    grid[row, gridCol + 1] = inputCell switch
                    {
                        EmptySpaceChar => EmptySpaceChar,
                        WallChar => WallChar,
                        BoxChar => RightBoxChar,
                        RobotChar => EmptySpaceChar,
                        _ => throw new ArgumentException(),
                    };
                }
            }

            return new Warehouse(height, width * 2, grid, robotPosition);
        }
    };
}
