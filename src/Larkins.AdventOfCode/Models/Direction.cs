namespace Larkins.AdventOfCode.Models;

public enum Direction
{
    Up = 0,
    Right = 1,
    Down = 2,
    Left = 3
}

public static class DirectionExtensions
{
    public static Direction Opposite(this Direction direction)
    {
        return direction switch
        {
            Direction.Up => Direction.Down,
            Direction.Down => Direction.Up,
            Direction.Left => Direction.Right,
            Direction.Right => Direction.Left,
            _ => throw new UnreachableException()
        };
    }

    public static int TurnsToDirection(
        this Direction startDirection,
        Direction desiredDirection)
    {
        var diff = Math.Abs(desiredDirection - startDirection);

        return diff switch
        {
            0 => 0,
            2 => 2,
            _ => 1
        };
    }
}
