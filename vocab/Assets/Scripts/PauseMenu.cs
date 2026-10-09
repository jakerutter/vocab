using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Button pauseButton;
    //public Animator aninmator;
    public CanvasGroup pausePanelCG;
    public CanvasGroup gamePanelCG;
    public CanvasGroup leaderboardPanelCG;
    //public TextMeshProUGUI playerName;

    void Start()
    {
        //playerName.text = $"{LeaderboardManager.Instance.GetPlayerName()}";
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            TogglePausePanel();
        }
    }

    public void PlayGame()
    {
        //aninmator.SetBool("PlayGame", true);
    }

    public void TogglePausePanel()
    {
        if (pausePanelCG.alpha == 0)
        {
            pausePanelCG.alpha = 1;
            pausePanelCG.interactable = true;
            pausePanelCG.blocksRaycasts = true;

            gamePanelCG.alpha = 0;
            gamePanelCG.interactable = false;
            gamePanelCG.blocksRaycasts = false;

            
            leaderboardPanelCG.alpha = 0;
            leaderboardPanelCG.interactable = false;
            leaderboardPanelCG.blocksRaycasts = false;
        }
        else
        {
            pausePanelCG.alpha = 0;
            pausePanelCG.interactable = false;
            pausePanelCG.blocksRaycasts = false;

            leaderboardPanelCG.alpha = 0;
            leaderboardPanelCG.interactable = false;
            leaderboardPanelCG.blocksRaycasts = false;

            gamePanelCG.alpha = 1;
            gamePanelCG.interactable = true;
            gamePanelCG.blocksRaycasts = true;
        }
    }

    public void ToggleLeaderboardPanel()
    {
        if (leaderboardPanelCG.alpha == 0)
        {
            leaderboardPanelCG.alpha = 1;
            leaderboardPanelCG.interactable = true;
            leaderboardPanelCG.blocksRaycasts = true;

            gamePanelCG.alpha = 0;
            gamePanelCG.interactable = false;
            gamePanelCG.blocksRaycasts = false;

            
            pausePanelCG.alpha = 0;
            pausePanelCG.interactable = false;
            pausePanelCG.blocksRaycasts = false;
        }
        else
        {
            leaderboardPanelCG.alpha = 0;
            leaderboardPanelCG.interactable = false;
            leaderboardPanelCG.blocksRaycasts = false;

            pausePanelCG.alpha = 0;
            pausePanelCG.interactable = false;
            pausePanelCG.blocksRaycasts = false;

            gamePanelCG.alpha = 1;
            gamePanelCG.interactable = true;
            gamePanelCG.blocksRaycasts = true;
        }
    }

    public void SaveScoreAndQuit()
    {
        if (LeaderboardManager.Instance != null)
        {
            LeaderboardManager.Instance.SaveScoreAndQuit();
        }
        else
        {
            Debug.LogWarning("LeaderboardManager.Instance was not found.");
        }
    }
}
