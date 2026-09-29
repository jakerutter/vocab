using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public Button playButton;
    //public Animator aninmator;
    public CanvasGroup settingsPanelCG;
    public CanvasGroup gamePanelCG;
    public CanvasGroup leaderboardPanelCG;
    public ImageFade titleImage;
    public TextMeshProUGUI playerName;

    void Start()
    {
        AudioManager.instance.Play("TickTock");
        titleImage.FadeIn();
        playerName.text = $"{LeaderboardManager.Instance.GetPlayerName()}";
    }

    public void PlayGame()
    {
        //aninmator.SetBool("PlayGame", true);
    }

    public void ToggleSettingsPanel()
    {
        if (settingsPanelCG.alpha == 0)
        {
            settingsPanelCG.alpha = 1;
            settingsPanelCG.interactable = true;
            settingsPanelCG.blocksRaycasts = true;

            gamePanelCG.alpha = 0;
            gamePanelCG.interactable = false;
            gamePanelCG.blocksRaycasts = false;

            
            leaderboardPanelCG.alpha = 0;
            leaderboardPanelCG.interactable = false;
            leaderboardPanelCG.blocksRaycasts = false;
        }
        else
        {
            settingsPanelCG.alpha = 0;
            settingsPanelCG.interactable = false;
            settingsPanelCG.blocksRaycasts = false;

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

            
            settingsPanelCG.alpha = 0;
            settingsPanelCG.interactable = false;
            settingsPanelCG.blocksRaycasts = false;
        }
        else
        {
            leaderboardPanelCG.alpha = 0;
            leaderboardPanelCG.interactable = false;
            leaderboardPanelCG.blocksRaycasts = false;

            settingsPanelCG.alpha = 0;
            settingsPanelCG.interactable = false;
            settingsPanelCG.blocksRaycasts = false;

            gamePanelCG.alpha = 1;
            gamePanelCG.interactable = true;
            gamePanelCG.blocksRaycasts = true;
        }
    }
}
