using UnityEngine;
using TMPro;
using System.Collections;
public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text gameScoreText;
    [SerializeField] private TMP_Text highScoreText;
    private void Start()
    {
        scoreText.text = "0";
    }
    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnUpdateUI,UpdateScoreUI);
        EventManager.AddHandler(GameEvent.OnDie,ShowGameOverPanel);
        EventManager.AddHandler(GameEvent.OnDie,()=>{gameScoreText.text = FindObjectOfType<ScoreManager>().GetScore().ToString();});
        EventManager.AddHandler(GameEvent.OnDie,()=>{highScoreText.text = FindObjectOfType<ScoreManager>().GetHighScore().ToString();});
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnUpdateUI, UpdateScoreUI);
        EventManager.RemoveHandler(GameEvent.OnDie, ShowGameOverPanel);
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

    public void RestartGame(){
        FindObjectOfType<SceneManagerScript>().LoadScene(0);
    }

}

