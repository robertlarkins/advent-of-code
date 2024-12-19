using Larkins.AdventOfCode.Utilities;

namespace Larkins.AdventOfCode.AdventOfCode2024.Day19LinenLayout;

public class Year2024Day19Part01Solver
{
    private Trie trie;
    private List<string> desiredDesigns = [];

    public Year2024Day19Part01Solver(IEnumerable<string> input)
    {
        var inputData = input.ToList();
        ParseInput(inputData);
    }

    public int Solve()
    {
        var count = 0;

        for (var i = 0; i < desiredDesigns.Count; i++)
        {
            Console.WriteLine($"start = {i}");

            if (IsPatternPossible(desiredDesigns[i]))
            {
                count++;
            }

            Console.WriteLine($"end = {i}");
        }

        return count;
        // return desiredDesigns.Count(IsPatternPossible);
    }

    private bool IsPatternPossible(string pattern)
    {
        var enqueues = 0;
        var dequeues = 0;
        var queue = new PriorityQueue<(int i, int j), int>(Comparer<int>.Create((existing, newPriority) => newPriority.CompareTo(existing)));
        queue.Enqueue((0, 1), 1);
        enqueues++;

        while (queue.Count > 0)
        {
            var (i, j) = queue.Dequeue();
            dequeues++;

            var word = pattern[i..j];

            var result = trie.SearchForWordAlsoIndicatingStart(word);

            if (result == WordSearchResult.NotFound)
            {
                continue;
            }

            if (result == WordSearchResult.StartOfWord)
            {
                if (j == pattern.Length)
                {
                    continue;
                }

                queue.Enqueue((i, j+1), j+1-i);
                enqueues++;

                continue;
            }

            if (result == WordSearchResult.Found)
            {
                if (i > 0)
                {
                    trie.AddWord(pattern[..j]);
                }

                if (j == pattern.Length)
                {
                    return true;
                }

                queue.Enqueue((j, j+1), 1);
                enqueues++;
            }

            if (result == WordSearchResult.FoundAndStartOfWord)
            {
                if (j == pattern.Length)
                {
                    return true;
                }

                queue.Enqueue((j, j + 1), 1);
                queue.Enqueue((i, j + 1), j+1-i);
                enqueues+=2;
            }
        }

        return false;
    }

    private void ParseInput(List<string> input)
    {
        var towelPatterns = input[0].Split(",", StringSplitOptions.TrimEntries);
        trie = new Trie();
        trie.AddWords(towelPatterns);

        desiredDesigns = input.Skip(2).ToList();
    }
}
