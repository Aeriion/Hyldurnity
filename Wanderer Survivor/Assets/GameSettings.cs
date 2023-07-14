using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance { get; private set; }

    public float soundVolume = 1f;
    public bool isMusicEnabled = true;

    private const string VolumeKey = "SoundVolume";
    private const string MusicEnabledKey = "IsMusicEnabled";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadSettings();
    }

    private void LoadSettings()
    {
        soundVolume = PlayerPrefs.GetFloat(VolumeKey, 1f);
        isMusicEnabled = PlayerPrefs.GetInt(MusicEnabledKey, 1) == 1;

        ApplySettings();
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat(VolumeKey, soundVolume);
        PlayerPrefs.SetInt(MusicEnabledKey, isMusicEnabled ? 1 : 0);
        PlayerPrefs.Save();

        ApplySettings();
    }

    public void ApplySettings()
    {
        AudioManager.Instance.SetSoundVolume(soundVolume);
        AudioManager.Instance.SetMusicState(isMusicEnabled);
    }
}