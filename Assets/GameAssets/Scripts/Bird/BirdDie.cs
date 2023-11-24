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
        var rb = GetComponent<Rigidbody2D>();
        var sprite = GetComponentInChildren<SpriteRenderer>();
        GetComponent<BirdMovement>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        sprite.sprite = deadBirdSprite;
        sprite.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        Destroy(this, 2f);
    }
}
