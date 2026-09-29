using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FeedbackManager : MonoBehaviour
{
    public TextMeshProUGUI feedbackText;
    public Animator animator;
    private List<string> positiveFeedbackList = new List<string>();
    private List<string> negativeFeedbackList = new List<string>();

    private void Start()
    {
        positiveFeedbackList.Add("Great!");
        positiveFeedbackList.Add("Good");
        positiveFeedbackList.Add("Wow!");
        positiveFeedbackList.Add("You're smart!");
        positiveFeedbackList.Add("Perfect");
        positiveFeedbackList.Add("Nailed it!");
        positiveFeedbackList.Add("Way to go");
        positiveFeedbackList.Add("Wonderful");
        positiveFeedbackList.Add("Nice :)");
        positiveFeedbackList.Add("Well done");

        negativeFeedbackList.Add("Nope");
        negativeFeedbackList.Add("Sorry!");
        negativeFeedbackList.Add("Ahh..");
        negativeFeedbackList.Add("Wrong");
        negativeFeedbackList.Add("So close");
        negativeFeedbackList.Add("Not quite");
        negativeFeedbackList.Add("Try again");
        negativeFeedbackList.Add("Keep going");
        negativeFeedbackList.Add("Almost");
        negativeFeedbackList.Add("Welp..");
    }

    public void ShowPositiveFeedback()
    {
        feedbackText.text = GetPositive_FeedbackText();
        animator.SetTrigger("ShowPostive_Feedback");
    }

    public void ShowNegativeFeedback()
    {
        feedbackText.text = GetNegative_FeedbackText();
        animator.SetTrigger("ShowNegative_Feedback");
    }

    public string GetPositive_FeedbackText()
    {
        int rand = UnityEngine.Random.Range(0, 9);
        return positiveFeedbackList[rand];
    }

    public string GetNegative_FeedbackText()
    {
        int rand = UnityEngine.Random.Range(0, 9);
        return negativeFeedbackList[rand];
    }
}
