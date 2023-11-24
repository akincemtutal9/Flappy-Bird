using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip jumpSound;    
    [SerializeField] private AudioClip pointSound;
    [SerializeField] private AudioClip dieSound;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip coinSound;

    private AudioSource musicSource;
    private AudioSource sfxSource;

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnJump, PlayJumpSound);
        EventManager.AddHandler(GameEvent.OnDie, PlayDieSound);
        EventManager.AddHandler(GameEvent.OnScore, PlayPointSound);
        EventManager.AddHandler(GameEvent.OnHit, PlayHitSound);
        EventManager.AddHandler(GameEvent.OnCoin, PlayCoinSound);
    }
    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnJump, PlayJumpSound);
        EventManager.RemoveHandler(GameEvent.OnDie, PlayDieSound);
        EventManager.RemoveHandler(GameEvent.OnScore, PlayPointSound);
        EventManager.RemoveHandler(GameEvent.OnHit, PlayHitSound);
        EventManager.RemoveHandler(GameEvent.OnCoin, PlayCoinSound);
    }
    private void Start()
    {
        PlayBackgroundMusic();
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        musicSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();
    }
    
    private void PlayBackgroundMusic()
    {
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.Play();
    }
    private void PlayDieSound()
    {
        sfxSource.PlayOneShot(dieSound);
    }
    private void PlayJumpSound()
    {
        sfxSource.PlayOneShot(jumpSound);
    }
    private void PlayPointSound()
    {
        sfxSource.PlayOneShot(pointSound);
    }
    private void PlayHitSound()
    {
        sfxSource.PlayOneShot(hitSound);
    }
    private void PlayCoinSound()
    {
        sfxSource.PlayOneShot(coinSound);
    }
}
