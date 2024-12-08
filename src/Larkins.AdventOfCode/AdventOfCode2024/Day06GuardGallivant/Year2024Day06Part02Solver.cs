using Larkins.AdventOfCode.Models;

namespace Larkins.AdventOfCode.AdventOfCode2024.Day06GuardGallivant;

public class Year2024Day06Part02Solver
{
    private readonly List<Point2D> ObstaclePositions = [];
    private Point2D GuardStartPosition;
    private Dictionary<int, SortedSet<int>> obstaclesByRow = new();
    private Dictionary<int, SortedSet<int>> obstaclesByCol = new();
    private int mapHeight;
    private int mapWidth;
    private List<string> input;

    public Year2024Day06Part02Solver(IEnumerable<string> input)
    {
        this.input = input.ToList();
        mapHeight = this.input.Count;
        mapWidth = this.input[0].Length;

        ParseInput();
    }
    
    public int Solve()
    {
        PopulateObstaclePositions();
        var patrolPath = CalculateFullPatrolPath();

        // The obstacle will go somewhere on the patrol path
        // Get the position before the obstacle
        // Recalculate path to see if any of the points endup back at this position.

        var triedObstaclePlaces = new HashSet<Point2D>
        {
            GuardStartPosition,
        };
        var obstaclePlaces = new HashSet<Point2D>();
        
        var loopCount = 0;

        for (var i = 0; i < patrolPath.Count; i++)
        {
            var obstacleToAdd = patrolPath[i].Position;
            if (!triedObstaclePlaces.Add(obstacleToAdd))
            {
                continue;
            }

            AddObstaclePosition(patrolPath[i].Position);
            
            // recalculate path and see if guard ends up back at patrolPath[i-1] with the same direction
            var isLoopCreated = IsLoopCreated();
            if (isLoopCreated)
            {
                obstaclePlaces.Add(obstacleToAdd);
                loopCount++;
            }
            
            RemoveObstaclePosition(patrolPath[i].Position);
        }

        return loopCount;
    }

    private bool IsLoopCreated()
    {
        var patrolPath = new HashSet<(Point2D, Direction)>();

        var guardPosition = GuardStartPosition;
        var guardDirection = Direction.Up;
        
        while (TryGetNextObstaclePosition(guardPosition, guardDirection, out var currentObstaclePosition))
        {
            guardPosition = guardDirection switch
            {
                Direction.Up => currentObstaclePosition! with { Y = currentObstaclePosition.Y + 1 },
                Direction.Down => currentObstaclePosition! with { Y = currentObstaclePosition.Y - 1 },
                Direction.Left => currentObstaclePosition! with { X = currentObstaclePosition.X + 1 },
                Direction.Right => currentObstaclePosition! with { X = currentObstaclePosition.X - 1 },
                _ => throw new UnreachableException()
            };

            var guardMovement = (guardPosition, guardDirection);
            
            if (!patrolPath.Add(guardMovement))
            {
                return true;
            }

            guardDirection = GetNextDirection(guardDirection);
        }

        return false;
    }

    private List<Point2D> CalculatePointsBetween(
        Point2D pointA,
        Point2D pointB)
    {
        var points = new List<Point2D>
        {
            pointA
        };
        
        var xDiff = pointA.X - pointB.X;
        var yDiff = pointA.Y - pointB.Y;
        
        var maxDiff = Math.Max(Math.Abs(xDiff), Math.Abs(yDiff));
        var xSign = Math.Sign(xDiff);
        var ySign = Math.Sign(yDiff);
        
        var currentPoint = pointA;

        for (var i = 0; i < maxDiff; i++)
        {
            currentPoint = new Point2D(currentPoint.Y - ySign, currentPoint.X - xSign);
            
            points.Add(currentPoint);
        }
        
        return points;
    }

    private List<(Point2D Position, Direction Direction)> CalculateFullPatrolPath()
    {
        var guardPosition = GuardStartPosition;
        var guardDirection = Direction.Up;
        
        var patrolPath = new List<(Point2D Position, Direction Direction)>
        {
            (guardPosition, guardDirection) 
        };
        
        while (TryGetNextObstaclePosition(guardPosition, guardDirection, out var currentObstaclePosition))
        {
            guardPosition = guardDirection switch
            {
                Direction.Up => currentObstaclePosition! with { Y = currentObstaclePosition.Y + 1 },
                Direction.Down => currentObstaclePosition! with { Y = currentObstaclePosition.Y - 1 },
                Direction.Left => currentObstaclePosition! with { X = currentObstaclePosition.X + 1 },
                Direction.Right => currentObstaclePosition! with { X = currentObstaclePosition.X - 1 },
                _ => throw new UnreachableException()
            };

            var pathBetween = CalculatePointsBetween(patrolPath.Last().Position, guardPosition);

            for (var i = 1; i < pathBetween.Count; i++)
            {
                patrolPath.Add((pathBetween[i], guardDirection));
            }
            
            guardDirection = GetNextDirection(guardDirection);
        }
        
        // add last position on map
        guardPosition = guardDirection switch
        {
            Direction.Up => guardPosition with {Y = 0},
            Direction.Down => guardPosition with {Y = mapHeight - 1},
            Direction.Left => guardPosition with {X = 0},
            Direction.Right => guardPosition with {X = mapWidth - 1},
            _ => throw new UnreachableException()
        };
        
        var pathBetween2 = CalculatePointsBetween(patrolPath.Last().Position, guardPosition);

        for (var i = 1; i < pathBetween2.Count; i++)
        {
            patrolPath.Add((pathBetween2[i], guardDirection));
        }

        return patrolPath;
    }

    private bool TryGetNextObstaclePosition(
        Point2D guardPosition,
        Direction guardDirection,
        out Point2D? obstaclePosition)
    {
        obstaclePosition = default;

        switch (guardDirection)
        {
            // Find the first obstacle in the direction the guard is going
            // For up, search in the same col, but row less than current position
            // For down, search in the same col, but row greater than current position
            // For left, search in the same row, but col less than current position
            // For right, search in the same row, but col greater than current position
            case Direction.Up:
            {
                var col = guardPosition.X;
                var row = guardPosition.Y;
                var hasObstacle = obstaclesByCol.TryGetValue(col, out var obstacleRowPositions);

                if (!hasObstacle)
                {
                    return false;
                }
            
                var closestRows = obstacleRowPositions!.GetViewBetween(0, row);
                
                if (closestRows.Count == 0)
                {
                    return false;
                }
                
                var closestRow = closestRows.Max;
                
                obstaclePosition = new Point2D(closestRow, col);
                return true;
            }
            case Direction.Down:
            {
                var col = guardPosition.X;
                var row = guardPosition.Y;
                var hasObstacle = obstaclesByCol.TryGetValue(col, out var obstacleRowPositions);

                if (!hasObstacle)
                {
                    return false;
                }
            
                var closestRows = obstacleRowPositions!.GetViewBetween(row, mapHeight);
                if (closestRows.Count == 0)
                {
                    return false;
                }
                
                var closestRow = closestRows.Min;

                obstaclePosition = new Point2D(closestRow, col);
                return true;
            }
            case Direction.Left:
            {
                var col = guardPosition.X;
                var row = guardPosition.Y;
                var hasObstacle = obstaclesByRow.TryGetValue(row, out var obstacleColPositions);

                if (!hasObstacle)
                {
                    return false;
                }
            
                var closestCols = obstacleColPositions!.GetViewBetween(0, col);
                
                if (closestCols.Count == 0)
                {
                    return false;
                }
                
                var closestCol = closestCols.Max;
                
                obstaclePosition = new Point2D(row, closestCol);
                return true;
            }
            case Direction.Right:
            {
                var col = guardPosition.X;
                var row = guardPosition.Y;
                var hasObstacle = obstaclesByRow.TryGetValue(row, out var obstacleColPositions);

                if (!hasObstacle)
                {
                    return false;
                }
            
                var closestCols = obstacleColPositions!.GetViewBetween(col, mapWidth);
                
                if (closestCols.Count == 0)
                {
                    return false;
                }
                
                var closestCol = closestCols.Min;

                obstaclePosition = new Point2D(row, closestCol);
                return true;
            }
            default:
                throw new UnreachableException();
        }
    }

    private Direction GetNextDirection(Direction currentDirection)
    {
        return currentDirection switch
        {
            Direction.Up => Direction.Right,
            Direction.Right => Direction.Down,
            Direction.Down => Direction.Left,
            Direction.Left => Direction.Up,
            _ => throw new UnreachableException()
        };
    }

    private void PopulateObstaclePositions()
    {
        foreach (var obstaclePosition in ObstaclePositions)
        {
            AddObstaclePosition(obstaclePosition);
        }
    }

    private void AddObstaclePosition(Point2D obstaclePosition)
    {
        var row = obstaclePosition.Y;
        var col = obstaclePosition.X;
            
        if (!obstaclesByRow.TryAdd(row, [col]))
        {
            obstaclesByRow[row].Add(col);
        }
            
        if (!obstaclesByCol.TryAdd(col, [row]))
        {
            obstaclesByCol[col].Add(row);
        }
    }

    private void RemoveObstaclePosition(Point2D obstaclePosition)
    {
        var row = obstaclePosition.Y;
        var col = obstaclePosition.X;

        if (obstaclesByRow.ContainsKey(row))
        {
            obstaclesByRow[row].Remove(col);
        }
            
        if (obstaclesByCol.ContainsKey(col))
        {
            obstaclesByCol[col].Remove(row);
        }
    }

    private enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    
    private void ParseInput()
    {
        var rows = input.ToList();

        for (var row = 0; row < mapHeight; row++)
        {
            var array = rows[row].ToArray();

            for (var col = 0; col < mapWidth; col++)
            {
                if (array[col] == '#')
                {
                    ObstaclePositions.Add(new Point2D(row, col));
                }

                if (array[col] == '^')
                {
                    GuardStartPosition = new Point2D(row, col);
                }
            }
        }
    }
}
