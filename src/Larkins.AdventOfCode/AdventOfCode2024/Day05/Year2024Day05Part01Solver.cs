namespace Larkins.AdventOfCode.AdventOfCode2024.Day05;

public class Year2024Day05Part01Solver
{
    List<(int pageA, int pageB)> pageOrderingRules = [];
    List<List<int>> pagesToUpdate = [];
    
    public int Solve(IEnumerable<string> input)
    {
        ParseInput(input);
        
        var dictionary = new Dictionary<int, HashSet<int>>();
        
        foreach (var rules in pageOrderingRules)
        {
            if (dictionary.ContainsKey(rules.pageA))
            {
                dictionary[rules.pageA].Add(rules.pageB);
            }
            else
            {
                var hashSet = new HashSet<int>
                {
                    rules.pageB
                };
                dictionary.Add(rules.pageA, hashSet);
            }
        }

        var indexes = new List<int>();
        
        // Check that each item is in order
        for (var p = 0; p < pagesToUpdate.Count; p++)
        {
            var page = pagesToUpdate[p];
            var isIndexToSkip = false;
            
            for (var i = 0; i < page.Count - 1; i++)
            {
                var p1 = page[i];
                var p2 = page[i + 1];
                if (!dictionary.ContainsKey(p1) || !dictionary[p1].Contains(p2))
                {
                    isIndexToSkip = true;
                    break;
                }
            }

            if (!isIndexToSkip)
            {
                indexes.Add(p);
            }
        }

        var value = 0;
        
        // get the middle items
        foreach (var index in indexes)
        {
            var middle = pagesToUpdate[index].Count / 2;
            value += pagesToUpdate[index][middle];
        }
 
        return value;
    }

    private void ParseInput(IEnumerable<string> input)
    {
        var isPagePairProcessing = true;
        
        foreach (var line in input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                isPagePairProcessing = false;
                continue;
            }

            if (isPagePairProcessing)
            {
                var pagesPairsLine = line.Split('|');
                var pageA = int.Parse(pagesPairsLine[0]);
                var pageB = int.Parse(pagesPairsLine[1]);
                pageOrderingRules.Add((pageA, pageB));
            }
            else
            {
                var pagesToUpdateSplit = line.Split(',');
                var pagesToUpdateList = pagesToUpdateSplit.Select(int.Parse).ToList();
                pagesToUpdate.Add(pagesToUpdateList);
            }
        }
    }
}