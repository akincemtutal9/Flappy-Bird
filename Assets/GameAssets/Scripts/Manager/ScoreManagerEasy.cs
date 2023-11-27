using UnityEngine;
using System.Collections.Generic;
using PlayFab.ClientModels;
using PlayFab;

public class ScoreManagerEasy : MonoBehaviour
{
    private int score = 0;
    private int highScore = 0;

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnScore, AddScore);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnScore, AddScore);
    }
    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScoreEasy", 0);
    }

    private void AddScore()
    {
        score++;
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScoreEasy", highScore);
            try
            {
                var request = new UpdatePlayerStatisticsRequest
                {
                    Statistics = new List<StatisticUpdate>
                {
                    new StatisticUpdate
                    {
                        StatisticName = "FlappyBird-Easy",
                        Value = highScore
                    }
                }
                };
                PlayFabClientAPI.UpdatePlayerStatistics(request, OnUpdatePlayerStatisticsSuccess, OnUpdatePlayerStatisticsError);
            }
            catch
            {
                Debug.Log("Error submitting score");
            }

        }
        EventManager.TriggerEvent(GameEvent.OnUpdateUI);
    }

    private void OnUpdatePlayerStatisticsSuccess(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successfully submitted score");
    }
    private void OnUpdatePlayerStatisticsError(PlayFabError error)
    {
        Debug.Log("Error submitting score");
        Debug.Log(error.GenerateErrorReport());
    }
    public int GetScore()
    {
        return score;
    }
    public int GetHighScore()
    {
        return highScore;
    }
}
