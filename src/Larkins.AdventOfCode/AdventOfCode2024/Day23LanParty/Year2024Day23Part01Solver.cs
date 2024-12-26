namespace Larkins.AdventOfCode.AdventOfCode2024.Day23LanParty;

public class Year2024Day23Part01Solver
{
    private readonly Dictionary<string, HashSet<string>> connections = [];

    public Year2024Day23Part01Solver(IEnumerable<string> input)
    {
        var inputData = input.ToList();
        ParseInput(inputData);
    }

    public int Solve()
    {
        var foundTriples = new HashSet<(string a, string b, string c)>();

        foreach (var connectionKvp in connections)
        {
            var computer1 = connectionKvp.Key;

            if (computer1[0] != 't')
            {
                continue;
            }

            var immediateConnections = connectionKvp.Value.ToList();

            var everyPair = immediateConnections
                .SelectMany((_, i) => immediateConnections.Skip(i + 1), (connection1, connection2) => (connection1, connection2));

            foreach (var (computer2, computer3) in everyPair)
            {
                if (connections[computer2].Contains(computer3))
                {
                    var computers = new List<string> { computer1, computer2, computer3 };
                    computers.Sort();

                    foundTriples.Add((computers[0], computers[1], computers[2]));
                }
            }
        }

        return foundTriples.Count;
    }

    private void ParseInput(List<string> input)
    {
        foreach (var line in input)
        {
            var computers = line.Split("-");
            var computer1 = computers[0];
            var computer2 = computers[1];

            if (!connections.TryAdd(computer1, [computer2]))
            {
                connections[computer1].Add(computer2);
            }

            if (!connections.TryAdd(computer2, [computer1]))
            {
                connections[computer2].Add(computer1);
            }
        }
    }
}
