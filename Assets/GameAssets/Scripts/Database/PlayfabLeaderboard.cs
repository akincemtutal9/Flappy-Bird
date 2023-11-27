using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class PlayfabLeaderboard : MonoBehaviour
{

   [SerializeField] private GameObject rowPrefab;
   [SerializeField] private Transform rowParent;

    private void Start()
    {
        GetLeaderboard();
    }

   public void SendLeaderBoard(int score){
         var request = new UpdatePlayerStatisticsRequest
         {
              Statistics = new List<StatisticUpdate>
              {
                   new StatisticUpdate
                   {
                        StatisticName = "FlappyBird",
                        Value = score
                   }
              }
         };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderBoardUpdate, OnLeaderBoardUpdateError);
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
    public void GetLeaderboard()
    {
        for(int i = 1;i<rowParent.childCount;i++){
            Destroy(rowParent.GetChild(i).gameObject);
        }
        
        var request = new GetLeaderboardRequest
        {
            StatisticName = "FlappyBird",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnLeaderboardGetError);
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
        }
    }

    private void OnLeaderboardGetError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }

}
