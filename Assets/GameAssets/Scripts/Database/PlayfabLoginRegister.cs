using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PlayfabLoginRegister : MonoBehaviour
{
    private const string RegisterScene = nameof(RegisterScene);
    private const string Main = nameof(Main);
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TMP_Text errorMessageText;
    [SerializeField] private TMP_Text successMessageText;
    [SerializeField] private Button login;
    private const string gmailMock = "@gmail.com";
    private string gmail = "";

    private void Start()
    {
        if(SceneManager.GetActiveScene().name != RegisterScene){
            LoginUser();  
        }
    }
    public void RegisterUser()
    {
        if (checkIfUsernameBlank() || checkIfUsernameHasBlanks())
        {
            return;
        }

        gmail = usernameInputField.text + gmailMock;
        var request = new RegisterPlayFabUserRequest
        {
            Email = gmail,
            Password = "12345678",
            DisplayName = usernameInputField.text,
            Username = usernameInputField.text,
            RequireBothUsernameAndEmail = false
        };
        successMessageText.text = "Registering...";
        errorMessageText.text = "";
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailure);
    }
    public void LoginUser()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = PlayerPrefs.GetString("EMAIL"),
            Password = PlayerPrefs.GetString("PASSWORD"),
        };
        try{
            errorMessageText.text = "";
            successMessageText.text = "Logging in...";
        }
        catch(Exception e){
            Debug.Log(e);
        }
        finally{
            PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
        }
    }
    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        successMessageText.text = "";
        PlayerPrefs.SetString("EMAIL", gmail);
        PlayerPrefs.SetString("PASSWORD", "12345678");
        PlayerPrefs.SetString("USERNAME", usernameInputField.text);
        LoginUser();
    }
    private void OnRegisterFailure(PlayFabError error)
    {
        Debug.LogError("Failed to register user: " + error.ErrorMessage);
        if(error.ErrorMessage.Contains("Email address not available")){
            errorMessageText.text = "Username already taken";   
        }else{
            errorMessageText.text = error.ErrorMessage;
        }
    }
    private void OnLoginSuccess(LoginResult result)
    {
        try{
            successMessageText.text = "";
        }
        catch(Exception e){
            Debug.Log(e);
        }
        finally{
            SceneManager.LoadScene(Main);
        }
    }
    private void OnLoginFailure(PlayFabError error)
    {
        SceneManager.LoadScene(RegisterScene);
    }
    private bool checkIfUsernameBlank()
    {
        if (usernameInputField.text.Equals(""))
        {
            errorMessageText.text = "Username cannot be blank";
            return true;
        }
        return false;
    }

    private bool checkIfUsernameHasBlanks()
    {
        if (usernameInputField.text.Contains(" "))
        {
            errorMessageText.text = "Username cannot contain blanks";
            return true;
        }
        return false;
    }
}


