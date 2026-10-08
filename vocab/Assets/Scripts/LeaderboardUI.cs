using UnityEngine;

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] private Transform scoresContainer;
    [SerializeField] private LeaderboardRowUI leaderboardRowPrefab;

    private void Start()
    {
        if (LeaderboardManager.Instance == null)
        {
            Debug.LogWarning("LeaderboardManager not found.");
            return;
        }

        LeaderboardManager.Instance.ScoresUpdated += DisplayScores;

        if (LeaderboardManager.Instance.LatestScores != null)
        {
            DisplayScores(LeaderboardManager.Instance.LatestScores);
        }
        else
        {
            LeaderboardManager.Instance.GetTopScores();
        }
    }

    private void OnDestroy()
    {
        if (LeaderboardManager.Instance != null)
        {
            LeaderboardManager.Instance.ScoresUpdated -= DisplayScores;
        }
    }

    public void Refresh()
    {
        if (LeaderboardManager.Instance != null)
        {
            LeaderboardManager.Instance.GetTopScores();
        }
    }

    private void DisplayScores(LeaderboardScore[] scores)
    {
        ClearLeaderboard();

        string currentPlayerName =
            LeaderboardManager.Instance.GetPlayerName();

        for (int i = 0; i < scores.Length; i++)
        {
            LeaderboardScore score = scores[i];

            LeaderboardRowUI row =
                Instantiate(leaderboardRowPrefab, scoresContainer);

            bool isCurrentPlayer =
                score.player_name == currentPlayerName;

            row.Setup(
                i + 1,
                score,
                isCurrentPlayer,
                i % 2 == 0
            );
        }
    }

    private void ClearLeaderboard()
    {
        foreach (Transform child in scoresContainer)
        {
            Destroy(child.gameObject);
        }
    }
}