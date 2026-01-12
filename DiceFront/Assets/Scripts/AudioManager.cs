using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Sound Effects")]
    public AudioClip winSFX;
    public AudioClip loseSFX;
    public AudioClip diceRollSFX;
    public AudioClip selectSFX;

    AudioSource audioSource;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlayWinSFX()
    {
        PlayClip(winSFX);
    }

    public void PlayLoseSFX()
    {
        PlayClip(loseSFX);
    }

    public void PlayDiceRollSFX()
    {
        PlayClip(diceRollSFX);
    }

    public void PlaySelectSFX()
    {
        PlayClip(selectSFX);
    }

    void PlayClip(AudioClip clip)
    {
        if (audioSource == null || clip == null)
            return;

        audioSource.PlayOneShot(clip);
    }
}
