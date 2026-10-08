using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Collections.Generic;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance { get; private set; }

    public event Action<LeaderboardScore[]> ScoresUpdated;

    public LeaderboardScore[] LatestScores { get; private set; }

    [Header("API Settings")]
    [SerializeField]
    private string leaderboardUrl =
        "https://simplecrm.dev/api/leaderboard_scores";

    [SerializeField]
    private string gameName = "vocab";

    private string sessionKey;
    private double gameStartTime;
    private const string PlayerNameKey = "LeaderboardPlayerName";

    private readonly string[] adjectives =
    {
        "Brave",
        "Bright",
        "Clever",
        "Cool",
        "Daring",
        "Eager",
        "Friendly",
        "Happy",
        "Helpful",
        "Jolly",
        "Kind",
        "Lucky",
        "Mighty",
        "Noble",
        "Quick",
        "Ready",
        "Shiny",
        "Smart",
        "Speedy",
        "Strong",
        "Sunny",
        "Super",
        "Swift",
        "Terrific",
        "Valiant",
        "Wise",
        "Witty",
        "Bold",
        "Cheerful",
        "Awesome"
    };

    private readonly string[] animals =
    {
        "Bear",
        "Beaver",
        "Bison",
        "Bunny",
        "Cardinal",
        "Cheetah",
        "Dolphin",
        "Eagle",
        "Falcon",
        "Fox",
        "Frog",
        "Giraffe",
        "Hawk",
        "Hedgehog",
        "Koala",
        "Lion",
        "Lynx",
        "Moose",
        "Otter",
        "Owl",
        "Panda",
        "Penguin",
        "Raccoon",
        "Robin",
        "Seal",
        "Tiger",
        "Turtle",
        "Wolf",
        "Zebra",
        "Squirrel"
    };

    private readonly HashSet<int> blockedNumbers = new HashSet<int>
    {
        666
    };

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        gameStartTime = Time.realtimeSinceStartupAsDouble;
    }

    private void Start()
    {
        gameStartTime = Time.realtimeSinceStartupAsDouble;
        sessionKey = Guid.NewGuid().ToString();
    }

    private string GetRandomAdjective()
    {
        return adjectives[
            UnityEngine.Random.Range(0, adjectives.Length)
        ];
    }

    private string GetRandomAnimal()
    {
        return animals[
            UnityEngine.Random.Range(0, animals.Length)
        ];
    }

    private string GetRandomDigits()
    {
        int number;

        do
        {
            number = UnityEngine.Random.Range(0, 1000);
        }
        while (blockedNumbers.Contains(number));

        return number.ToString("D3");
    }

    private string GeneratePlayerName()
    {
        return $"{GetRandomAdjective()}{GetRandomAnimal()}-{GetRandomDigits()}";
    }

    public string GetPlayerName()
    {
        if (PlayerPrefs.HasKey(PlayerNameKey))
        {
            return PlayerPrefs.GetString(PlayerNameKey);
        }

        string playerName = GeneratePlayerName();

        PlayerPrefs.SetString(PlayerNameKey, playerName);
        PlayerPrefs.Save();

        return playerName;
    }

    public void GetTopScores()
    {
        StartCoroutine(GetTopScoresCoroutine());
    }

    private IEnumerator GetTopScoresCoroutine()
    {
        string url =
            leaderboardUrl +
            "?game_name=" +
            UnityWebRequest.EscapeURL(gameName);

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(
                    $"Leaderboard GET failed: {request.error}"
                );

                yield break;
            }

            Debug.Log(
                $"Leaderboard response: {request.downloadHandler.text}"
            );

            LeaderboardResponse response =
                JsonUtility.FromJson<LeaderboardResponse>(
                    request.downloadHandler.text
                );

            if (response?.scores == null)
            {
                Debug.LogWarning("No leaderboard scores returned.");
                yield break;
            }

            LatestScores = response.scores;

            foreach (LeaderboardScore score in LatestScores)
            {
                Debug.Log(
                    $"{score.player_name}: {score.score} " +
                    $"(Streak: {score.max_streak})"
                );
            }

            ScoresUpdated?.Invoke(LatestScores);
        }
    }

    public int GetDurationSeconds()
    {
        return (int)(Time.realtimeSinceStartupAsDouble - gameStartTime);
    }

    public void PostScore()
    {
        int duration = GetDurationSeconds();
        string playerName = GetPlayerName();
        string playerKey = "testKey";
        int score = ScoreKeeper.totalScore;
        int maxStreak = ScoreKeeper.GetMaxStreak();
        int questionsAnswered = ScoreKeeper.GetAnswersSubmitted();
        int correctAnswers = ScoreKeeper.GetCorrectAnswersCount();

        LeaderboardScorePayload payload =
            new LeaderboardScorePayload
            {
                game_name = gameName,
                player_name = playerName,
                player_key = playerKey,
                score = score,
                max_streak = maxStreak,
                difficulty = null,
                game_mode = null,
                questions_answered = questionsAnswered,
                correct_answers = correctAnswers,
                duration_seconds = duration,
                client_version = Application.version,
                metadata = new LeaderboardMetadata
                {
                    session_key = sessionKey
                }
            };

        StartCoroutine(PostScoreCoroutine(payload));
    }

    private IEnumerator PostScoreCoroutine(LeaderboardScorePayload payload)
    {
        LeaderboardScoreRequest body =
            new LeaderboardScoreRequest
            {
                leaderboard_score = payload
            };

        string json = JsonUtility.ToJson(body);

        Debug.Log($"Posting leaderboard score: {json}");

        byte[] jsonBytes = Encoding.UTF8.GetBytes(json);

        using (UnityWebRequest request =
               new UnityWebRequest(leaderboardUrl, "POST"))
        {
            request.uploadHandler =
                new UploadHandlerRaw(jsonBytes);

            request.downloadHandler =
                new DownloadHandlerBuffer();

            request.SetRequestHeader(
                "Content-Type",
                "application/json"
            );

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(
                    $"Leaderboard POST failed: " +
                    $"{request.responseCode} {request.error}"
                );

                Debug.LogError(
                    request.downloadHandler.text
                );

                yield break;
            }

            Debug.Log(
                $"Score submitted successfully: " +
                request.downloadHandler.text
            );

            GetTopScores();
        }
    }
}

// ----------------------------
// JSON MODELS
// ----------------------------

[Serializable]
public class LeaderboardResponse
{
    public LeaderboardScore[] scores;
}

[Serializable]
public class LeaderboardScore
{
    public int id;

    public string game_name;
    public string player_name;
    public string player_key;

    public int score;
    public int max_streak;

    public string difficulty;
    public string game_mode;

    public int questions_answered;
    public int correct_answers;
    public int duration_seconds;

    public string client_version;

    public string created_at;
    public string updated_at;
}

[Serializable]
public class LeaderboardScoreRequest
{
    public LeaderboardScorePayload leaderboard_score;
}

[Serializable]
public class LeaderboardScorePayload
{
    public string game_name;
    public string player_name;
    public string player_key;

    public int score;
    public int max_streak;

    public string difficulty;
    public string game_mode;

    public int questions_answered;
    public int correct_answers;
    public int duration_seconds;

    public string client_version;
    public LeaderboardMetadata metadata;
}