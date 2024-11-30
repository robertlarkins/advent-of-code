namespace Larkins.AdventOfCode.Utilities;

/// <summary>
/// A Trie is a type of search tree.
/// https://en.wikipedia.org/wiki/Trie
/// </summary>
public class Trie
{
    private Node root = new();

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

    public bool HasWord(string word)
    {
        var currentNode = root;
        foreach (var @char in word)
        {
            var hasNextCharNode = currentNode!.Children.TryGetValue(@char, out currentNode);

            if (!hasNextCharNode)
            {
                return false;
            }
        }

        return currentNode!.IsWord;
    }

    private class Node
    {
        public bool IsWord { get; set; }

        public Dictionary<char, Node> Children { get; } = [];
    }
}