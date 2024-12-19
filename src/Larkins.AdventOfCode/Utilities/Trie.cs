namespace Larkins.AdventOfCode.Utilities;

public enum WordSearchResult
{
    Found,
    StartOfWord,
    NotFound,
}

/// <summary>
/// A Trie is a type of search tree.
/// https://en.wikipedia.org/wiki/Trie
/// </summary>
public class Trie
{
    private readonly Node root = new();

    public void AddWord(string word)
    {
        var currentNode = root;
        foreach (var @char in word)
        {
            var hasNextCharNode = currentNode!.Children.TryGetValue(@char, out var nextCharNode);

            if (!hasNextCharNode)
            {
                nextCharNode = new Node();
                currentNode.Children.Add(@char, nextCharNode);
            }

            currentNode = nextCharNode;
        }

        currentNode!.IsWord = true;
    }

    public void AddWords(IEnumerable<string> words)
    {
        foreach (var word in words)
        {
            AddWord(word);
        }
    }

    public WordSearchResult SearchForWord(string word)
    {
        var currentNode = root;
        foreach (var @char in word)
        {
            var hasNextCharNode = currentNode!.Children.TryGetValue(@char, out currentNode);

            if (!hasNextCharNode)
            {
                return WordSearchResult.NotFound;
            }
        }

        return currentNode!.IsWord
            ? WordSearchResult.Found
            : WordSearchResult.StartOfWord;
    }

    private class Node
    {
        public bool IsWord { get; set; }

        public Dictionary<char, Node> Children { get; } = [];
    }
}
