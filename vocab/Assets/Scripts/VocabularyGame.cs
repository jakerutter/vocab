
using System;
using System.Collections.Generic;
using System.Linq;

public class VocabularyGame
{
    private readonly Random random = new Random();

    private List<VocabularyWord> eligibleWords;
    private Queue<VocabularyWord> questionQueue;
    private HashSet<string> incorrectGuesses;

    public VocabularyQuestion CurrentQuestion { get; private set; }

    public int Score { get; private set; }
    public int Streak { get; private set; }
    public int BestStreak { get; private set; }
    public int QuestionsAnswered { get; private set; }
    public int FirstAttemptCorrect { get; private set; }

    public float ElapsedSeconds { get; private set; }
    public bool IsPlaying { get; private set; }

    public void StartGame(
        List<VocabularyWord> vocabulary,
        GameSettings settings)
    {
        if (settings.Grade < 1 || settings.Grade > 12)
            throw new ArgumentOutOfRangeException(
                nameof(settings.Grade));

        eligibleWords = vocabulary
            .Where(w => settings.IncludeLowerGrades
                ? w.Grade <= settings.Grade
                : w.Grade == settings.Grade)
            .GroupBy(w => w.Word, StringComparer.OrdinalIgnoreCase)
            .Select(g => g.First())
            .ToList();

        if (eligibleWords.Count < 4)
            throw new InvalidOperationException(
                "At least four unique words are required.");

        Score = 0;
        Streak = 0;
        BestStreak = 0;
        QuestionsAnswered = 0;
        FirstAttemptCorrect = 0;
        ElapsedSeconds = 0;
        IsPlaying = true;

        questionQueue = new Queue<VocabularyWord>();
        NextQuestion();
    }

    public void NextQuestion()
    {
        if (!IsPlaying)
            return;

        if (questionQueue.Count == 0)
        {
            questionQueue = new Queue<VocabularyWord>(
                eligibleWords.OrderBy(w => random.Next()));
        }

        VocabularyWord correct = questionQueue.Dequeue();

        var distractors = eligibleWords
            .Where(w => !string.Equals(
                w.Word, correct.Word,
                StringComparison.OrdinalIgnoreCase))
            .OrderBy(w => random.Next())
            .Take(3)
            .Select(w => w.Word);

        var choices = distractors
            .Append(correct.Word)
            .OrderBy(w => random.Next())
            .ToList();

        CurrentQuestion = new VocabularyQuestion
        {
            CorrectWord = correct,
            Choices = choices
        };

        incorrectGuesses = new HashSet<string>(
            StringComparer.OrdinalIgnoreCase);
    }

    public AnswerResult SubmitAnswer(string selectedWord)
    {
        if (!IsPlaying || CurrentQuestion == null)
            throw new InvalidOperationException(
                "There is no active question.");

        if (!CurrentQuestion.Choices.Contains(
            selectedWord, StringComparer.OrdinalIgnoreCase))
            throw new ArgumentException(
                "The selected word is not a valid choice.");

        if (!string.Equals(
            selectedWord,
            CurrentQuestion.CorrectWord.Word,
            StringComparison.OrdinalIgnoreCase))
        {
            // Repeated clicks on the same incorrect answer
            // should not cause multiple penalties.
            if (incorrectGuesses.Add(selectedWord))
                Streak = 0;

            return AnswerResult.Incorrect;
        }

        bool firstAttempt = incorrectGuesses.Count == 0;

        if (firstAttempt)
        {
            Score += 100;
            Streak++;
            FirstAttemptCorrect++;

            if (Streak % 5 == 0)
                Score += 100;

            BestStreak = Math.Max(BestStreak, Streak);
        }
        else
        {
            Score += 25;
        }

        QuestionsAnswered++;
        CurrentQuestion = null;

        return firstAttempt
            ? AnswerResult.CorrectFirstAttempt
            : AnswerResult.CorrectAfterRetry;
    }

    public void Tick(float deltaTime)
    {
        if (IsPlaying)
            ElapsedSeconds += Math.Max(0, deltaTime);
    }

    public void EndGame()
    {
        IsPlaying = false;
        CurrentQuestion = null;
    }
}