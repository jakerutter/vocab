using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreKeeper
{
    public static int scoreStreak = 0;
    public static int totalScore = 0;
    private static int maxStreak = 0;
    private static int answersSubmitted = 0;
    private static int correctAnswersCount = 0;

    public static void AddToScore()
    {
        totalScore += 1;
        scoreStreak += 1;
        correctAnswersCount += 1;
        answersSubmitted += 1;
        if (scoreStreak > maxStreak)
        {
            maxStreak = scoreStreak;
        }
    }

    public static void AddAnswersSubmitted()
    {
        // even when wrong we iterate this
        answersSubmitted += 1;
    }

    public static int GetMaxStreak()
    {
        return maxStreak;
    }

    public static int GetCorrectAnswersCount()
    {
        return correctAnswersCount;
    }

    public static int GetAnswersSubmitted()
    {
        return answersSubmitted;
    }
}
