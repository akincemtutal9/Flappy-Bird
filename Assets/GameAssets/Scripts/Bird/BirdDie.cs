using UnityEngine;

public class BirdDie : MonoBehaviour
{
    [SerializeField] private Sprite deadBirdSprite;
    [SerializeField] private float force = 2f;

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnDie, KillBird);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnDie, KillBird);        
    }
    private void KillBird()
    {
        GetComponent<BirdMovement>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponentInChildren<SpriteRenderer>().sprite = deadBirdSprite;
        GetComponentInChildren<SpriteRenderer>().transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * force, ForceMode2D.Impulse);
        Destroy(this, 2f);
    }
}
