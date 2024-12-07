namespace Larkins.AdventOfCode.AdventOfCode2024.Day05;

public class Year2024Day05Part02Solver
{
    List<(int pageA, int pageB)> pageOrderingRules = [];
    List<List<int>> pagesToUpdate = [];
    Dictionary<int, HashSet<int>> dictionaryOfPageOrderingRules;
    
    public int Solve(IEnumerable<string> input)
    {
        ParseInput(input);
        
        dictionaryOfPageOrderingRules = PopulateDictionaryOfPageAndPagesThatOccurAfter();

        var value = 0;
        
        // Check that each item is in order
        for (var p = 0; p < pagesToUpdate.Count; p++)
        {
            var page = pagesToUpdate[p];
            
            var isPageOrderCorrect = IsPageOrderCorrect(page);

            if (!isPageOrderCorrect)
            {
                CorrectPageOrder(page);
                value += GetMiddleValue(page);
            }
        }

        return value;
    }

    private List<int> CorrectPageOrder2(List<int> pageOrder)
    {
        var newOrder = new List<int>();
        
        while (pageOrder.Count > 1)
        {
            var item = pageOrder[0];
            
            var otherItems = pageOrder[1..];

            if (dictionaryOfPageOrderingRules.ContainsKey(item))
            {
                var hashSet = dictionaryOfPageOrderingRules[item];
                
                foreach (var otherItem in otherItems)
                {
                    if (!hashSet.Contains(otherItem))
                    {
                        pageOrder.RemoveAt(0);
                        pageOrder.Add(item);
                        break;
                    }
                }
                
            }
            
            // is item before every other item
            // for (var j = i + 1; j < pageOrder.Count; i++)
            // {
            //     item.
            // }   
        }

        return [];
    }
    
    private void CorrectPageOrder(List<int> pageOrder)
    {
        for (var i = 0; i < pageOrder.Count - 1; i++)
        {
            var p1 = pageOrder[i];
            var p2 = pageOrder[i + 1];

            if (!IsPagePairOrderCorrect(p1, p2))
            {
                // next item is in the wrong place
                if (i == 0)
                {
                    // put first item on the end
                    var item = pageOrder[i];
                    pageOrder.RemoveAt(i);
                    pageOrder.Add(item);
                    i = -1;
                }

                for (var j = i + 2; j < pageOrder.Count; j++)
                {
                    var isNowCorrect = IsPagePairOrderCorrect(p1, pageOrder[j]);
                    if (isNowCorrect)
                    {
                        (pageOrder[i + 1], pageOrder[j]) = (pageOrder[j], pageOrder[i + 1]);
                        break;
                    }
                }
            }
        }

        bool IsPagePairOrderCorrect(int p1, int p2)
        {
            return dictionaryOfPageOrderingRules.ContainsKey(p1) && dictionaryOfPageOrderingRules[p1].Contains(p2);
        }
    }

    private int GetMiddleValue(List<int> pageOrder)
    {
        var middle = pageOrder.Count / 2;

        return pageOrder[middle];
    }
    
    private bool IsPageOrderCorrect(List<int> pageOrder)
    {
        for (var i = 0; i < pageOrder.Count - 1; i++)
        {
            var p1 = pageOrder[i];
            var p2 = pageOrder[i + 1];

            if (!dictionaryOfPageOrderingRules.ContainsKey(p1) || !dictionaryOfPageOrderingRules[p1].Contains(p2))
            {
                return false;
            }
        }

        return true;
    }

    private Dictionary<int, HashSet<int>> PopulateDictionaryOfPageAndPagesThatOccurAfter()
    {
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
        
        return dictionary;
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