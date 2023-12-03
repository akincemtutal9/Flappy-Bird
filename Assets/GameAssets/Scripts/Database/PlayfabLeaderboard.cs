using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class PlayfabLeaderboard : MonoBehaviour
{
    [SerializeField] private GameObject rowPrefab;
    [SerializeField] private Transform rowParent;
    [SerializeField] private TMP_Text leaderboardNameText;
    private string leaderboardName = "FlappyBird";

    private void Start()
    {
        UpdateDatabaseWithLocalScores();
        GetLeaderboard(leaderboardName);
    }
    private void OnLeaderBoardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successfully submitted score");
    }

    private void OnLeaderBoardUpdateError(PlayFabError error)
    {
        Debug.Log("Error submitting score");
        Debug.Log(error.GenerateErrorReport());
    }
    public void GetLeaderboard(string leaderboardName)
    {
        for (int i = 1; i < rowParent.childCount; i++)
        {
            Destroy(rowParent.GetChild(i).gameObject);
        }

        var request = new GetLeaderboardRequest
        {
            StatisticName = leaderboardName,
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnLeaderboardGetError);
        leaderboardNameText.text = leaderboardName + " Leaderboard\n" + "TOP 10";
    }
    private void OnLeaderboardGet(GetLeaderboardResult result)
    {
        foreach (var item in result.Leaderboard)
        {
            var row = Instantiate(rowPrefab, rowParent);
            TextMeshProUGUI[] texts = row.GetComponentsInChildren<TextMeshProUGUI>();
            texts[1].text = (item.Position + 1).ToString();
            texts[2].text = item.DisplayName;
            texts[3].text = item.StatValue.ToString();
            if(item.PlayFabId == PlayFabSettings.staticPlayer.PlayFabId){
                foreach (var text in texts)
                {
                    text.color = Color.yellow;
                }
            }
        }
    }
    private void OnLeaderboardGetError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }
    private void UpdateDatabaseWithLocalScores()
    {
        if (GetMyScore("FlappyBird") < PlayerPrefs.GetInt("HighScore"))
        {
            var request = new UpdatePlayerStatisticsRequest
            {
                Statistics = new System.Collections.Generic.List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "FlappyBird",
                    Value = PlayerPrefs.GetInt("HighScore")
                }
            }
            };
            PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderBoardUpdate, OnLeaderBoardUpdateError);
        }
        if (GetMyScore("FlappyBird-Easy") < PlayerPrefs.GetInt("HighScoreEasy"))
        {
            var request2 = new UpdatePlayerStatisticsRequest
            {
                Statistics = new System.Collections.Generic.List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "FlappyBird-Easy",
                    Value = PlayerPrefs.GetInt("HighScoreEasy")
                }
            }
            };
            PlayFabClientAPI.UpdatePlayerStatistics(request2, OnLeaderBoardUpdate, OnLeaderBoardUpdateError);
        }
    }
    public int GetMyScore(string leaderboardName)
    {
        int myScore = 0;

        var request = new GetLeaderboardAroundPlayerRequest
        {
            StatisticName = leaderboardName,
            MaxResultsCount = 1
        };

        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, result =>
        {
            if (result.Leaderboard.Count > 0)
            {
                myScore = result.Leaderboard[0].StatValue;
            }
        }, OnLeaderboardGetError);

        return myScore;
    }
}
