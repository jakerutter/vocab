
using System;
using System.Collections.Generic;

public abstract class VocabularyGrade
{
    private readonly Dictionary<string, string> _words;

    protected VocabularyGrade(Dictionary<string, string> words)
    {
        _words = words;
    }
    
    public IEnumerable<KeyValuePair<string, string>> Entries => _words;

    public bool TryGetDefinition(
        string word,
        out string definition)
    {
        if (string.IsNullOrWhiteSpace(word))
        {
            definition = null;
            return false;
        }

        return _words.TryGetValue(word.Trim(), out definition);
    }
}