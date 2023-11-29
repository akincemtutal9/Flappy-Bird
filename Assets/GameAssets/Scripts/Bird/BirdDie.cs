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
        Destroy(this.gameObject, 2f); // Destroy the GameObject, not the script
        PlayerPrefs.SetInt("DieCount", PlayerPrefs.GetInt("DieCount") + 1);
        Debug.Log("Die Count: " + PlayerPrefs.GetInt("DieCount"));
        TriggerInterAd();
    }
    private void TriggerInterAd()
    {
        if (PlayerPrefs.GetInt("DieCount") > 0 && PlayerPrefs.GetInt("DieCount") % 5 == 0)
        {
            Debug.Log("Show Rewarded Ad");
            EventManager.TriggerEvent(GameEvent.ShowRewardAd);
        }
    }
}
