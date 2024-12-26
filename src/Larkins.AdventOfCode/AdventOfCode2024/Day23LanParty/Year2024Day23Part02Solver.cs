using Larkins.AdventOfCode.Models;

namespace Larkins.AdventOfCode.AdventOfCode2024.Day23LanParty;

public class Year2024Day23Part02Solver
{
    private readonly Dictionary<string, HashSet<string>> connections = [];

    public Year2024Day23Part02Solver(IEnumerable<string> input)
    {
        var inputData = input.ToList();
        ParseInput(inputData);
    }

    public string Solve()
    {
        var graph = new Graph(connections);
        var largestClique = graph.FindLargestClique();
        largestClique.Sort();
        var joined = string.Join(",", largestClique);

        Console.WriteLine($"Largest Clique: {joined}");

        return joined;
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
