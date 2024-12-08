using Larkins.AdventOfCode.Models;

namespace Larkins.AdventOfCode.AdventOfCode2024.Day06GuardGallivant;

public class Year2024Day06Part01Solver
{
    private readonly List<GridPoint> ObstaclePositions = [];
    private GridPoint GuardStartPosition;
    private Dictionary<int, SortedSet<int>> obstaclesByRow = new();
    private Dictionary<int, SortedSet<int>> obstaclesByCol = new();
    private int mapHeight;
    private int mapWidth;
    private List<string> input;

    public Year2024Day06Part01Solver(IEnumerable<string> input)
    {
        this.input = input.ToList();
        mapHeight = this.input.Count;
        mapWidth = this.input[0].Length;

        ParseInput();
    }

    public int Solve()
    {
        var visitedPositions = new HashSet<GridPoint>();

        PopulateObstaclePositions();
        var patrolPath = CalculateGuardPatrolPath();

        // go through patrol path and add items to visitedPositions
        for (var i = 0; i < patrolPath.Count - 1; i++)
        {
            var pointA = patrolPath[i];
            var pointB = patrolPath[i + 1];
            
            var pointsBetween = CalculatePointsBetween(pointA, pointB);
            
            visitedPositions.UnionWith(pointsBetween);
        }
        
        return visitedPositions.Count;
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
    
    private List<GridPoint> CalculateGuardPatrolPath()
    {
        var patrolPath = new List<GridPoint>
        {
            GuardStartPosition
        };

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

            patrolPath.Add(guardPosition);
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
        
        patrolPath.Add(guardPosition);

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