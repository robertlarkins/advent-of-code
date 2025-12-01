namespace Larkins.AdventOfCode.AdventOfCode2025.Day01SecretEntrance;

public class Year2025Day01Part01Solver
{
    private readonly List<Rotation> rotations = [];

    public Year2025Day01Part01Solver(IEnumerable<string> input)
    {
        foreach (var line in input)
        {
            rotations.Add(ParseInputLine(line));
        }
    }

    public int Solve()
    {
        var currentPosition = 50;
        var count = 0;

        foreach (var rotation in rotations)
        {
            // left rotation
            if (rotation.Direction == 'L')
            {
                // numbers go down
                currentPosition -= rotation.Distance;

                if (currentPosition < 0)
                {
                    currentPosition += 100;
                }
            }

            // right rotation
            if (rotation.Direction == 'R')
            {
                currentPosition += rotation.Distance;
            }

            currentPosition %= 100;

            if (currentPosition == 0)
            {
                count++;
            }
        }

        return count;
    }


    private Rotation ParseInputLine(string line)
    {
        var direction = line[0];
        var distance = int.Parse(line[1 ..]);

        return new Rotation(direction, distance);
    }

    public record Rotation(char Direction, int Distance);

}
