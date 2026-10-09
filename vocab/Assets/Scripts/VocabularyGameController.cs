using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class VocabularyGameController : MonoBehaviour
{
    //ui pieces
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI headerSubText;
    public TextMeshProUGUI gradeSelectedText;
    public List<TextMeshProUGUI> answerTextList;
    public List<Button> answerButtonList;
    public TextMeshProUGUI totalScore;
    public TextMeshProUGUI correctStreak;
    public FeedbackManager feedbackManager;
    private VocabularyGame game;

    private void Awake()
    {
        game = new VocabularyGame();
    }

    private void Start()
    {
      Debug.Log("Current grade is " + MasterSettings.selectedGrade.ToString());
      //set score ui to 0
      totalScore.text = "0";
      correctStreak.text = "0";

      gradeSelectedText.text = "grade " + MasterSettings.selectedGrade.ToString();
      ApplyTextColor();

      StartGame();
    }

    private void Update()
    {
        if (game != null && game.IsPlaying)
            game.Tick(Time.deltaTime);
    }

    public void StartGame()
    {
        GameSettings settings = new GameSettings
        {
            Grade = MasterSettings.selectedGrade,
        };

        var vocabulary = VocabularyLoader.LoadGrade(settings.Grade);

        game.StartGame(vocabulary, settings);

        ShowCurrentQuestion();
    }

    public void SubmitAnswer(Button clickedButton)
    {
        TextMeshProUGUI buttonText = clickedButton.GetComponentInChildren<TextMeshProUGUI>();
        string selectedAnswer = buttonText.text;
        AnswerResult result = game.SubmitAnswer(selectedAnswer);

        Debug.Log($"Selected answer: {selectedAnswer}");
        Debug.Log($"Answer result: {result}");
        //Debug.Log($"Score: {game.Score}");
        //Debug.Log($"Streak: {game.Streak}");

        if (result == AnswerResult.CorrectFirstAttempt)
        {
          HandleCorrectAnswer();
          Debug.Log(selectedAnswer + " was the correct answer. Score is " + ScoreKeeper.GetCorrectAnswersCount());
        }
        else if (result == AnswerResult.CorrectAfterRetry)
        {
          ScoreKeeper.AddAnswersSubmitted();
        } 
        else
        {
          ScoreKeeper.ClearStreak();
          HandleIncorrectAnswer(clickedButton);
        }

        if (result == AnswerResult.CorrectFirstAttempt ||
            result == AnswerResult.CorrectAfterRetry)
        {
            EnableButtons();
            game.NextQuestion();
            ShowCurrentQuestion();
        }
    }

    private void HandleCorrectAnswer()
    {
        ScoreKeeper.AddToScore();
        totalScore.text = ScoreKeeper.totalScore.ToString();
        correctStreak.text = ScoreKeeper.scoreStreak.ToString();
        AudioManager.instance.PlayOneShot("Correct");
        feedbackManager.ShowPositiveFeedback();

        if(ScoreKeeper.totalScore % 3 == 0)
        {
            LeaderboardManager.Instance.PostScore();
        }
    }

    private void HandleIncorrectAnswer(Button clickedButton)
    {
        DisableSingleButton(clickedButton);
        ScoreKeeper.AddAnswersSubmitted();
        ScoreKeeper.scoreStreak = 0;
        correctStreak.text = ScoreKeeper.scoreStreak.ToString();
        //Debug.Log("Incorrect!");
        AudioManager.instance.PlayOneShot("Incorrect");
        feedbackManager.ShowNegativeFeedback();
    }

    private void ShowCurrentQuestion()
    {
        VocabularyQuestion question = game.CurrentQuestion;

        questionText.text = question.CorrectWord.Definition;

        //Debug.Log($"Definition: {question.CorrectWord.Definition}");

        for (int i = 0; i < question.Choices.Count && i < answerTextList.Count; i++)
        {
            answerTextList[i].text = question.Choices[i];
            //Debug.Log($"Choice {i}: {question.Choices[i]}");
        }
    }

    private void DisableSingleButton(Button button)
    {
        button.interactable = false;
    }

    private void DisableButtons()
    {
        for (int i = 0; i< answerButtonList.Count; i++)
        {
            answerButtonList[i].interactable = false;
        }
    }

    private void EnableButtons()
    {
        for (int i = 0; i< answerButtonList.Count; i++)
        {
            answerButtonList[i].interactable = true;
        }
    }

    public void EndGame()
    {
        game.EndGame();
    }

    private void ApplyTextColor()
    {
        Color selectedColor = MasterSettings.GetTextColor();

        questionText.color = selectedColor;

        foreach (TextMeshProUGUI answerText in answerTextList)
        {
            answerText.color = selectedColor;
        }
    }
}