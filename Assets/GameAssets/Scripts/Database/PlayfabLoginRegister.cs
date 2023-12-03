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
    [SerializeField] private TMP_InputField emailInputField;
    [SerializeField] private TMP_InputField passwordInputField;
    [SerializeField] private TMP_Text errorMessageText;
    [SerializeField] private TMP_Text successMessageText;
    
    private void Start()
    {
        if(SceneManager.GetActiveScene().name != RegisterScene){
            TryLogin();  
        }
    }

    public void RegisterUser()
    {
        if (checkIfUsernameBlank() || checkIfUsernameHasBlanks())
        {
            return;
        }
        var request = new RegisterPlayFabUserRequest
        {
            Email = emailInputField.text,
            Password = passwordInputField.text,
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
            Email = emailInputField.text,
            Password = passwordInputField.text ,
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
        successMessageText.text = "";
    }
    private void OnLoginSuccess(LoginResult result)
    {
        try{
            PlayerPrefs.SetString("EMAIL", emailInputField.text);
            PlayerPrefs.SetString("PASSWORD", passwordInputField.text);
            successMessageText.text = "";
            errorMessageText.text = "";
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
        errorMessageText.text = error.ErrorMessage;
        successMessageText.text = "";
    }

    public void TryLogin(){
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


