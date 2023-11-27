using TMPro;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class MainUI : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    void Start()
    {
        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), result =>
        {
            nameText.text = "Welcome " + "<color=yellow>" + result.AccountInfo.Username + "</color>";
        }, error => Debug.LogError(error.GenerateErrorReport()));
    }

}
