using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    public Toggle musicToggle;
    private bool isMusicEnabled = true;
    private AudioSource[] backgroundMusicSources;

    private const string MusicEnabledKey = "MusicEnabled";

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
    }

    private void Start()
    {
        backgroundMusicSources = GameObject.FindObjectsOfType<AudioSource>();

        musicToggle.isOn = PlayerPrefs.GetInt(MusicEnabledKey, 1) == 1;
        ToggleMusic(musicToggle.isOn);
    }

    public void ToggleMusic(bool isMusicEnabled)
    {
        this.isMusicEnabled = isMusicEnabled;

        PlayerPrefs.SetInt(MusicEnabledKey, isMusicEnabled ? 1 : 0);

        foreach (AudioSource backgroundMusicSource in backgroundMusicSources)
        {
            if (isMusicEnabled)
            {
                backgroundMusicSource.Play();
            }
            else
            {
                backgroundMusicSource.Stop();
            }
        }
    }
}