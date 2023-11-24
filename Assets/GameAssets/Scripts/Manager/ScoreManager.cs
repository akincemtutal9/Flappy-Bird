using UnityEngine;

public class ScoreManager : MonoBehaviour
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
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    private void AddScore()
    {
        score++;
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        EventManager.TriggerEvent(GameEvent.OnUpdateUI);
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
