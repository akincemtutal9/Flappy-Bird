using UnityEngine;
using Lean.Touch;   
public class GameStateManager : MonoBehaviour
{
    private bool isGameStarted;
    
    private void Start()
    {
        isGameStarted = false;
        Time.timeScale = 0;
    }
    private void OnEnable()
	{
		LeanTouch.OnFingerTap += HandleFingerTap;
	}
	private void OnDisable()
	{
		LeanTouch.OnFingerTap -= HandleFingerTap;
	}
    private void HandleFingerTap(LeanFinger finger)
	{
        StartGame();
        EventManager.TriggerEvent(GameEvent.OnUpdateUI);
        LeanTouch.OnFingerTap -= HandleFingerTap;
    }
    private void StartGame()
    {
        isGameStarted = true;
        Time.timeScale = 1;
    }
}
