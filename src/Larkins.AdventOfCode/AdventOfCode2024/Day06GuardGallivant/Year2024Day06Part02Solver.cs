using Larkins.AdventOfCode.Models;

namespace Larkins.AdventOfCode.AdventOfCode2024.Day06GuardGallivant;

public class Year2024Day06Part02Solver
{
    private readonly List<GridPoint> ObstaclePositions = [];
    private GridPoint GuardStartPosition;
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

        var triedObstaclePlaces = new HashSet<GridPoint>
        {
            GuardStartPosition,
        };
        var obstaclePlaces = new HashSet<GridPoint>();
        
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
        var patrolPath = new HashSet<(GridPoint, Direction)>();

        var guardPosition = GuardStartPosition;
        var guardDirection = Direction.Up;
        
        while (TryGetNextObstaclePosition(guardPosition, guardDirection, out var currentObstaclePosition))
        {
            guardPosition = guardDirection switch
            {
                Direction.Up => currentObstaclePosition! with { Row = currentObstaclePosition.Row + 1 },
                Direction.Down => currentObstaclePosition! with { Row = currentObstaclePosition.Row - 1 },
                Direction.Left => currentObstaclePosition! with { Col = currentObstaclePosition.Col + 1 },
                Direction.Right => currentObstaclePosition! with { Col = currentObstaclePosition.Col - 1 },
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

    private List<GridPoint> CalculatePointsBetween(
        GridPoint pointA,
        GridPoint pointB)
    {
        var points = new List<GridPoint>
        {
            pointA
        };
        
        var xDiff = pointA.Col - pointB.Col;
        var yDiff = pointA.Row - pointB.Row;
        
        var maxDiff = Math.Max(Math.Abs(xDiff), Math.Abs(yDiff));
        var xSign = Math.Sign(xDiff);
        var ySign = Math.Sign(yDiff);
        
        var currentPoint = pointA;

        for (var i = 0; i < maxDiff; i++)
        {
            currentPoint = new GridPoint(currentPoint.Row - ySign, currentPoint.Col - xSign);
            
            points.Add(currentPoint);
        }
        
        return points;
    }

    private List<(GridPoint Position, Direction Direction)> CalculateFullPatrolPath()
    {
        var guardPosition = GuardStartPosition;
        var guardDirection = Direction.Up;
        
        var patrolPath = new List<(GridPoint Position, Direction Direction)>
        {
            (guardPosition, guardDirection) 
        };
        
        while (TryGetNextObstaclePosition(guardPosition, guardDirection, out var currentObstaclePosition))
        {
            guardPosition = guardDirection switch
            {
                Direction.Up => currentObstaclePosition! with { Row = currentObstaclePosition.Row + 1 },
                Direction.Down => currentObstaclePosition! with { Row = currentObstaclePosition.Row - 1 },
                Direction.Left => currentObstaclePosition! with { Col = currentObstaclePosition.Col + 1 },
                Direction.Right => currentObstaclePosition! with { Col = currentObstaclePosition.Col - 1 },
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
            Direction.Up => guardPosition with {Row = 0},
            Direction.Down => guardPosition with {Row = mapHeight - 1},
            Direction.Left => guardPosition with {Col = 0},
            Direction.Right => guardPosition with {Col = mapWidth - 1},
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
        GridPoint guardPosition,
        Direction guardDirection,
        out GridPoint? obstaclePosition)
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
                var col = guardPosition.Col;
                var row = guardPosition.Row;
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
                
                obstaclePosition = new GridPoint(closestRow, col);
                return true;
            }
            case Direction.Down:
            {
                var col = guardPosition.Col;
                var row = guardPosition.Row;
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

                obstaclePosition = new GridPoint(closestRow, col);
                return true;
            }
            case Direction.Left:
            {
                var col = guardPosition.Col;
                var row = guardPosition.Row;
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
                
                obstaclePosition = new GridPoint(row, closestCol);
                return true;
            }
            case Direction.Right:
            {
                var col = guardPosition.Col;
                var row = guardPosition.Row;
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

                obstaclePosition = new GridPoint(row, closestCol);
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

    private void AddObstaclePosition(GridPoint obstaclePosition)
    {
        var row = obstaclePosition.Row;
        var col = obstaclePosition.Col;
            
        if (!obstaclesByRow.TryAdd(row, [col]))
        {
            obstaclesByRow[row].Add(col);
        }
            
        if (!obstaclesByCol.TryAdd(col, [row]))
        {
            obstaclesByCol[col].Add(row);
        }
    }

    private void RemoveObstaclePosition(GridPoint obstaclePosition)
    {
        var row = obstaclePosition.Row;
        var col = obstaclePosition.Col;

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
                    ObstaclePositions.Add(new GridPoint(row, col));
                }

                if (array[col] == '^')
                {
                    GuardStartPosition = new GridPoint(row, col);
                }
            }
        }
    }
}
