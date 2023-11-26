using UnityEngine;
using Lean.Touch;   
public class GameStateManager : MonoBehaviour
{
    private void Awake()
    {
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
        Time.timeScale = 1;
    }
}
