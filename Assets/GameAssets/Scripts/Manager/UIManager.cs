using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [Header("ScoreText")]
    [SerializeField] private TMP_Text scoreText;
    
    [Header("GameOverPanel")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text gameScoreText;
    [SerializeField] private TMP_Text highScoreText;

    [Header("StartPanel")]
    [SerializeField] private GameObject startPanel;

    private void Start()
    {
        scoreText.text = "0";
    }
    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnUpdateUI,UpdateScoreUI);
        EventManager.AddHandler(GameEvent.OnUpdateUI,StartGame);
        EventManager.AddHandler(GameEvent.OnDie,ShowGameOverPanel);
        EventManager.AddHandler(GameEvent.OnDie,UpdateScoreText);
        EventManager.AddHandler(GameEvent.OnDie,UpdateHighScoreText);
    }
    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnUpdateUI, UpdateScoreUI);
        EventManager.RemoveHandler(GameEvent.OnDie, ShowGameOverPanel);
        EventManager.RemoveHandler(GameEvent.OnUpdateUI,StartGame);
        EventManager.RemoveHandler(GameEvent.OnDie,UpdateScoreText);
        EventManager.RemoveHandler(GameEvent.OnDie,UpdateHighScoreText);
    }
    private void UpdateScoreUI(){
        if(SceneManager.GetActiveScene().name == "Game"){
            scoreText.text = FindObjectOfType<ScoreManager>().GetScore().ToString();
        }
        else if(SceneManager.GetActiveScene().name == "GameEasy"){
            scoreText.text = FindObjectOfType<ScoreManagerEasy>().GetScore().ToString();
        }
    }
    private void UpdateHighScoreText(){
        if(SceneManager.GetActiveScene().name == "Game"){
            highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
            Debug.Log("HighScore: " + PlayerPrefs.GetInt("HighScore"));
        }
        else if(SceneManager.GetActiveScene().name == "GameEasy"){
            highScoreText.text = PlayerPrefs.GetInt("HighScoreEasy").ToString();
            Debug.Log("HighScoreEasy: " + PlayerPrefs.GetInt("HighScoreEasy"));
        }
    }
    private void UpdateScoreText(){
        if(SceneManager.GetActiveScene().name == "Game"){
            gameScoreText.text = FindObjectOfType<ScoreManager>().GetScore().ToString();
        }
        else if(SceneManager.GetActiveScene().name == "GameEasy"){
            gameScoreText.text = FindObjectOfType<ScoreManagerEasy>().GetScore().ToString();
        }
    }
    private void ShowGameOverPanel(){
        StartCoroutine(WaitForSeconds(2f));
    }
    private IEnumerator WaitForSeconds(float time){
        yield return new WaitForSeconds(time);
        gameOverPanel.SetActive(true);
    }
    public void StartGame(){
        startPanel.SetActive(false);
    }
}

