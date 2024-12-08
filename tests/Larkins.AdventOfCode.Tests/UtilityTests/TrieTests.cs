using FluentAssertions.Execution;
using Larkins.AdventOfCode.Utilities;

namespace Larkins.AdventOfCode.Tests.UtilityTests;

public class TrieTests
{
    // Words
    private const string Word = "word";
    private const string Treatment = "treatment";
    private const string Treat = "treat";
    private const string Nope = "nope";
    private const string Meat = "meat";
    private const string Seat = "seat";
    
    // Performance tests
    // - only the minimum number of nodes needed are present. Can this be tested?

    [Fact]
    public void Trie_contains_added_word()
    {
        
        var sut = new Trie();
        sut.AddWord(Word);

        var result = sut.SearchForWord(Word);

        result.Should().Be(WordSearchResult.Found);
    }

    [Fact]
    public void Same_word_added_separately_to_trie_makes_no_changes()
    {
        var sut = new Trie();
        
        sut.AddWord(Word);
        sut.AddWord(Word);
        
        var result = sut.SearchForWord(Word);

        result.Should().Be(WordSearchResult.Found);
    }
    
    [Fact]
    public void Same_word_added_to_trie_makes_no_changes()
    {
        var sut = new Trie();
        
        sut.AddWords([Word, Word]);
        
        var result = sut.SearchForWord(Word);

        result.Should().Be(WordSearchResult.Found);
    }

    [Fact]
    public void Trie_contains_both_added_words_when_the_second_word_is_part_of_the_first_word()
    {
        var sut = new Trie();
        sut.AddWords([Treat, Treatment]);
        
        using (new AssertionScope())
        {
            sut.SearchForWord(Treatment).Should().Be(WordSearchResult.Found);
            sut.SearchForWord(Treat).Should().Be(WordSearchResult.Found);
        }
    }
    
    [Fact]
    public void Trie_contains_both_added_words_when_the_first_word_is_part_of_the_second_word()
    {
        var sut = new Trie();
        sut.AddWords([Treat, Treatment]);

        using (new AssertionScope())
        {
            sut.SearchForWord(Treatment).Should().Be(WordSearchResult.Found);
            sut.SearchForWord(Treat).Should().Be(WordSearchResult.Found);
        }
    }

    [Fact]
    public void Trie_does_not_contain_word_that_has_not_been_added()
    {
        var sut = new Trie();
       
        var result = sut.SearchForWord(Nope);

        result.Should().Be(WordSearchResult.NotFound);
    }
    
    /// <summary>
    /// A searched for word not in the trie could be the start of another word.
    /// Knowing this can be helpful if adding to the search word might find a word.
    /// </summary>
    [Fact]
    public void Trie_indicates_when_search_word_is_start_of_added_word()
    {
        var sut = new Trie();
        sut.AddWord(Treatment);
        
        var result = sut.SearchForWord(Treat);

        result.Should().Be(WordSearchResult.StartOfWord);
    }

    [Fact]
    public void Trie_contains_all_added_words()
    {
        var sut = new Trie();
        sut.AddWords([Meat, Seat, Treat]);

        using (new AssertionScope())
        {
            sut.SearchForWord(Meat).Should().Be(WordSearchResult.Found);
            sut.SearchForWord(Treat).Should().Be(WordSearchResult.Found);
            sut.SearchForWord(Seat).Should().Be(WordSearchResult.Found);
        }
    }
    
    [Fact]
    public void Trie_contains_all_separately_added_words()
    {
        var sut = new Trie();
        sut.AddWord(Meat);
        sut.AddWord(Treat);
        sut.AddWord(Seat);

        using (new AssertionScope())
        {
            sut.SearchForWord(Meat).Should().Be(WordSearchResult.Found);
            sut.SearchForWord(Treat).Should().Be(WordSearchResult.Found);
            sut.SearchForWord(Seat).Should().Be(WordSearchResult.Found);
        }
    }
}