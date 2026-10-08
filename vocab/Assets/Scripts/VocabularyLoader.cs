using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class VocabularyLoader
{
    // Assets/Resources/Vocabulary/grade3.json is loaded as "Vocabulary/grade3".
    // Resources assets are bundled in a Unity WebGL build.
    public static List<VocabularyWord> LoadGrade(int grade)
    {
        ValidateGrade(grade);

        string resourcePath = $"grade{grade}";
        TextAsset asset = Resources.Load<TextAsset>(resourcePath);
        if (asset == null)
            throw new InvalidOperationException(
                $"Missing vocabulary asset: Assets/Resources/{resourcePath}.json");

        VocabularyGradeJson data =
            JsonUtility.FromJson<VocabularyGradeJson>(asset.text);

        if (data == null || data.grade != grade || data.words == null)
            throw new InvalidOperationException(
                $"Invalid vocabulary JSON for grade {grade}.");

        var results = new List<VocabularyWord>(data.words.Count);
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (VocabularyEntryJson entry in data.words)
        {
            if (entry == null || string.IsNullOrWhiteSpace(entry.word) ||
                string.IsNullOrWhiteSpace(entry.definition) ||
                string.IsNullOrWhiteSpace(entry.partOfSpeech))
            {
                throw new InvalidOperationException(
                    $"Grade {grade} has a word with missing required fields.");
            }

            string word = entry.word.Trim();
            if (!seen.Add(word))
                throw new InvalidOperationException(
                    $"Duplicate word '{word}' within grade {grade}.");

            results.Add(new VocabularyWord
            {
                Word = word,
                Definition = entry.definition.Trim(),
                Grade = grade,
                PartOfSpeech = entry.partOfSpeech.Trim(),
                RelatedWords = CleanList(entry.relatedWords),
                Synonyms = CleanList(entry.synonyms),
                Antonyms = CleanList(entry.antonyms)
            });
        }

        return results;
    }

    public static List<VocabularyWord> LoadEligibleWords(
        int selectedGrade, bool includeLowerGrades)
    {
        ValidateGrade(selectedGrade);

        int firstGrade = includeLowerGrades ? 1 : selectedGrade;
        var allWords = new List<VocabularyWord>();

        for (int grade = firstGrade; grade <= selectedGrade; grade++)
            allWords.AddRange(LoadGrade(grade));

        // If a word occurs in multiple grades, use its entry in the
        // highest selected grade so it isn't asked twice per cycle.
        return allWords
            .GroupBy(w => w.Word, StringComparer.OrdinalIgnoreCase)
            .Select(group => group.OrderByDescending(w => w.Grade).First())
            .ToList();
    }

    public static Dictionary<string, VocabularyWord> BuildLookup(
        IEnumerable<VocabularyWord> words)
    {
        if (words == null)
            throw new ArgumentNullException(nameof(words));

        return words.ToDictionary(
            word => word.Word,
            word => word,
            StringComparer.OrdinalIgnoreCase);
    }

    private static List<string> CleanList(List<string> values)
    {
        if (values == null)
            return new List<string>();

        return values
            .Where(value => !string.IsNullOrWhiteSpace(value))
            .Select(value => value.Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();
    }

    private static void ValidateGrade(int grade)
    {
        if (grade < 1 || grade > 12)
            throw new ArgumentOutOfRangeException(
                nameof(grade), "Grade must be from 1 through 12.");
    }
}
