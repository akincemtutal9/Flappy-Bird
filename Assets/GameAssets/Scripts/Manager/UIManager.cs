using UnityEngine;
using TMPro;
using System.Collections;
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
        EventManager.AddHandler(GameEvent.OnDie,()=>{gameScoreText.text = FindObjectOfType<ScoreManager>().GetScore().ToString();});
        EventManager.AddHandler(GameEvent.OnDie,()=>{highScoreText.text = FindObjectOfType<ScoreManager>().GetHighScore().ToString();});
    }
    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnUpdateUI, UpdateScoreUI);
        EventManager.RemoveHandler(GameEvent.OnDie, ShowGameOverPanel);
        EventManager.RemoveHandler(GameEvent.OnUpdateUI,StartGame);
        EventManager.RemoveHandler(GameEvent.OnDie,()=>{gameScoreText.text = FindObjectOfType<ScoreManager>().GetScore().ToString();});
        EventManager.RemoveHandler(GameEvent.OnDie,()=>{highScoreText.text = FindObjectOfType<ScoreManager>().GetHighScore().ToString();});
    }
    private void UpdateScoreUI(){
        scoreText.text = FindObjectOfType<ScoreManager>().GetScore().ToString();
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

