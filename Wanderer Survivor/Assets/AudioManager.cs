using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField]
    private AudioSource[] soundSources;

    [SerializeField]
    private AudioSource musicSource;

    private float soundVolume = 1f;
    private bool isMusicEnabled = true;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetSoundVolume(float volume)
    {
        soundVolume = volume;
        UpdateSoundVolume();
    }

    public void SetMusicState(bool isEnabled)
    {
        isMusicEnabled = isEnabled;
        UpdateMusicState();
    }

    private void UpdateSoundVolume()
    {
        foreach (AudioSource source in soundSources)
        {
            source.volume = soundVolume;
        }
    }

    private void UpdateMusicState()
    {
        if (isMusicEnabled)
        {
            musicSource.volume = soundVolume;
            musicSource.Play();
        }
        else
        {
            musicSource.Stop();
        }
    }
}