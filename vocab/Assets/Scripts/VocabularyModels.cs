using System;
using System.Collections.Generic;

// These DTOs match the lowerCamelCase field names in grade3.json.
// Unity's JsonUtility serializes public fields, not C# properties.
[Serializable]
public sealed class VocabularyGradeJson
{
    public int grade;
    public List<VocabularyEntryJson> words;
}

[Serializable]
public sealed class VocabularyEntryJson
{
    public string word;
    public string definition;
    public string partOfSpeech;
    public List<string> relatedWords;
    public List<string> synonyms;
    public List<string> antonyms;
}

// Runtime model: preserves Word, Definition, and Grade from our
// earlier VocabularyGame design, so the game engine can use it directly.
public sealed class VocabularyWord
{
    public string Word;
    public string Definition;
    public int Grade;
    public string PartOfSpeech;
    public List<string> RelatedWords;
    public List<string> Synonyms;
    public List<string> Antonyms;
}

public sealed class VocabularyQuestion
{
    public VocabularyWord CorrectWord;
    public List<string> Choices;
}

public sealed class GameSettings
{
    public int Grade;
    public bool IncludeLowerGrades;
}

public enum AnswerResult
{
    Incorrect,
    CorrectFirstAttempt,
    CorrectAfterRetry
}
