namespace Larkins.AdventOfCode.AdventOfCode2025.Day01SecretEntrance;

public class Year2025Day01Part02Solver
{
    private readonly List<Rotation> rotations = [];

    public Year2025Day01Part02Solver(IEnumerable<string> input)
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
            var rot = rotation.Distance;
            count += rot / 100;
            rot %= 100;

            if (rot == 0)
            {
                continue;
            }

            // left rotation
            if (rotation.Direction == 'L')
            {
                var isCurrentPositionZero = currentPosition == 0;

                // numbers go down
                currentPosition -= rot;

                if (currentPosition < 0)
                {
                    if (!isCurrentPositionZero)
                    {
                        count++;
                    }

                    currentPosition += 100;
                }
            }

            // right rotation
            if (rotation.Direction == 'R')
            {
                currentPosition += rot;

                if (currentPosition > 100)
                {
                    count++;
                }
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
