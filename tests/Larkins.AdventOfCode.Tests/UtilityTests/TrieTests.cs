using FluentAssertions;
using FluentAssertions.Execution;
using Larkins.AdventOfCode.Utilities;

namespace Larkins.AdventOfCode.Tests.UtilityTests;

public class TrieTests
{
    // Performance tests
    // - only the minimum number of nodes needed are present. Can this be tested?

    [Fact]
    public void Trie_contains_added_word()
    {
        const string word = "word";
        var sut = new Trie();
        sut.AddWord(word);

        var result = sut.HasWord(word);

        result.Should().BeTrue();
    }

    [Fact]
    public void Same_word_added_to_trie_makes_no_changes()
    {
        var sut = new Trie();
        const string word = "word";
        
        sut.AddWord(word);
        sut.AddWord(word);
        
        var result = sut.HasWord(word);

        result.Should().BeTrue();
    }
    
    [Fact]
    public void Trie_contains_both_added_words_when_the_second_word_is_part_of_the_first_word()
    {
        const string treatment = "treatment";
        const string treat = "treat";
        
        var sut = new Trie();
        sut.AddWord(treatment);
        sut.AddWord(treat);
        
        var hasTreatment = sut.HasWord(treatment);
        var hasTreat = sut.HasWord(treat);

        using (new AssertionScope())
        {
            hasTreatment.Should().BeTrue();
            hasTreat.Should().BeTrue();
        }
    }
    
    [Fact]
    public void Trie_contains_both_added_words_when_the_first_word_is_part_of_the_second_word()
    {
        const string treatment = "treatment";
        const string treat = "treat";
        
        var sut = new Trie();
        sut.AddWord(treat);
        sut.AddWord(treatment);
        
        var hasTreatment = sut.HasWord(treatment);
        var hasTreat = sut.HasWord(treat);

        using (new AssertionScope())
        {
            hasTreatment.Should().BeTrue();
            hasTreat.Should().BeTrue();
        }
    }

    [Fact]
    public void Trie_does_not_contain_word_that_has_not_been_added()
    {
        var sut = new Trie();
       
        var result = sut.HasWord("nope");

        result.Should().BeFalse();
    }
    
    [Fact]
    public void Trie_does_not_contain_word_that_an_added_word_starts_with()
    {
        var sut = new Trie();
        sut.AddWord("treatment");
        
        var result = sut.HasWord("treat");

        result.Should().BeFalse();
    }
}