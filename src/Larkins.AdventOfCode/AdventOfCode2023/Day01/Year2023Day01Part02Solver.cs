using Larkins.AdventOfCode.Utilities;

namespace Larkins.AdventOfCode.AdventOfCode2023.Day01;

public class Year2023Day01Part02Solver
{
    private Trie trie = new();
    private Dictionary<string, int> digitWords = new()
    {
        { "one", 1 },
        { "two", 2 },
        { "three", 3 },
        { "four", 4 },
        { "five", 5 },
        { "six", 6 },
        { "seven", 7 },
        { "eight", 8 },
        { "nine", 9 } 
    };
    
    public Year2023Day01Part02Solver()
    {
        trie.AddWords(digitWords.Keys);
    }
    
    public int Solve(IEnumerable<string> input) => input.Sum(ExtractCalibrationValueSeparateConversions);

    private int ExtractCalibrationValueSeparateConversions(string line)
    {
        var lineLeftToRight = ConvertWordsToDigitsLeftToRight(line);
        var lineRightToLeft = ConvertWordsToDigitsRightToLeft(line);
        
        var firstDigit = ExtractFirstDigit(lineLeftToRight);
        var lastDigit = ExtractLastDigit(lineRightToLeft);
        
        return firstDigit * 10 + lastDigit;
    }

    private string ConvertWordsToDigitsLeftToRight(string line)
    {
        for (var i = 0; i < line.Length; i++)
        {
            line = LookForWordFromPosition(line, i);
        }

        return line;
    }

    private string LookForWordFromPosition(
        string line,
        int position)
    {
        for (var j = position; j < line.Length; j++)
        {
            var word = line.Substring(position, j + 1 - position);
            var searchResult = trie.SearchForWord(word);

            if (searchResult == WordSearchResult.NotFound)
            {
                break;
            }

            if (searchResult != WordSearchResult.Found)
            {
                continue;
            }

            return ReplaceWordWithDigit(word, line, position);
        }

        return line;
    }
    
    private string ConvertWordsToDigitsRightToLeft(string line)
    {
        for (var i = line.Length - 1; i >= 0; i--)
        {
            line = LookForWordFromPosition(line, i);
        }

        return line;
    }
    
    private string ReplaceWordWithDigit(
        string word,
        string line,
        int startPosition)
    {
        var positionAfterWord = startPosition + word.Length;        
        var wordDigit = digitWords[word];
        var textBeforeWord = line[..startPosition];
        var textAfterWord = line[positionAfterWord..];
        
        return textBeforeWord + wordDigit + textAfterWord;
    }
    
    private int ExtractFirstDigit(string line)
    {
        var firstDigit1 = -1;
        foreach (var t in line.Where(char.IsDigit))
        {
            firstDigit1 = int.Parse(t.ToString());
            break;
        }

        if (firstDigit1 == -1)
        {
            throw new Exception();
        }

        return firstDigit1;
    }
    
    private int ExtractLastDigit(string line)
    {
        var lastDigit1 = -1;
        for (var i = line.Length -1; i >= 0; i--)
        {
            if (char.IsDigit(line[i]))
            {
                lastDigit1 = int.Parse(line[i].ToString());
                break;
            }
        }

        if (lastDigit1 == -1)
        {
            throw new Exception();
        }

        return lastDigit1;
    }
}