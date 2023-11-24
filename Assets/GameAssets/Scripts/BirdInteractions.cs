using UnityEngine;

public class BirdInteractions : MonoBehaviour
{
    private const string Obstacle = nameof(Obstacle);
    private const string PointArea = nameof(PointArea);
    private const string Coin = nameof(Coin);

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.tag)
        {
            case Obstacle:
                EventManager.TriggerEvent(GameEvent.OnHit);
                EventManager.TriggerEvent(GameEvent.OnDie);
                break;
            case PointArea:
                EventManager.TriggerEvent(GameEvent.OnScore);
                break;
            case Coin:
                EventManager.TriggerEvent(GameEvent.OnCoin);
                Destroy(other.gameObject);
                break;
        }
    }
}
