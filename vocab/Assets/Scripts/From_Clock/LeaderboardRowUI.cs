using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardRowUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rankText;
    [SerializeField] private TextMeshProUGUI playerText;
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private Image background;
    [SerializeField] private Image medalImage;

    [SerializeField] private Color normalColor;
    [SerializeField] private Color alternateColor;
    [SerializeField] private GameObject currentPlayerIndicator_Left;
    [SerializeField] private GameObject currentPlayerIndicator_Right;
    [SerializeField] private Sprite goldMedal;
    [SerializeField] private Sprite silverMedal;
    [SerializeField] private Sprite bronzeMedal;

    public void Setup(
        int rank,
        LeaderboardScore score,
        bool isCurrentPlayer,
        bool alternateRow)
    {
        rankText.text = GetRankText(rank);
        playerText.text = score.player_name;
        scoreText.text = score.score.ToString();
        background.color =
            alternateRow ? alternateColor : normalColor;
        
        if (isCurrentPlayer)
        {
            Debug.Log("Found current player match on rank " + rank.ToString());
            currentPlayerIndicator_Left.GetComponent<ImageFade>().FadeIn();
            currentPlayerIndicator_Right.GetComponent<ImageFade>().FadeIn();    
        }

        switch (rank)
        {
            case 1:
                medalImage.sprite = goldMedal;
                medalImage.gameObject.SetActive(true);
                break;

            case 2:
                medalImage.sprite = silverMedal;
                medalImage.gameObject.SetActive(true);
                break;

            case 3:
                medalImage.sprite = bronzeMedal;
                medalImage.gameObject.SetActive(true);
                break;

            default:
                medalImage.sprite = null;
                medalImage.gameObject.SetActive(false);
                break;
        }
    }

    private string GetRankText(int rank)
    {
        switch (rank)
        {
            case 1:
                return "";

            case 2:
                return "";

            case 3:
                return "";

            default:
                return rank.ToString() + "th";
        }
    }
}