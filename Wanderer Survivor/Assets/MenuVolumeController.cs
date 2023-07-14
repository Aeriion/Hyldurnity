using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MenuVolumeController : MonoBehaviour
{
    [SerializeField]
    private Slider volumeSlider;

    [SerializeField]
    private Toggle musicToggle;

    private const string VolumeKey = "SoundVolume";
    private const string MusicEnabledKey = "IsMusicEnabled";

    private void Start()
    {
        volumeSlider.onValueChanged.AddListener(OnVolumeSliderChanged);
        musicToggle.onValueChanged.AddListener(OnMusicToggleChanged);

        ApplyVolumeSettings();
        ApplyMusicSettings();
    }

    private void OnVolumeSliderChanged(float value)
    {
        PlayerPrefs.SetFloat(VolumeKey, value);
        PlayerPrefs.Save();

        UpdateSoundVolume(value);
        UpdateMusicVolume(value);
    }

    private void OnMusicToggleChanged(bool isOn)
    {
        int musicEnabled = isOn ? 1 : 0;
        PlayerPrefs.SetInt(MusicEnabledKey, musicEnabled);
        PlayerPrefs.Save();

        ApplyMusicSettings();
    }

    private void ApplyVolumeSettings()
    {
        float volume = PlayerPrefs.GetFloat(VolumeKey, 1f);
        volumeSlider.value = volume;
        UpdateSoundVolume(volume);
        UpdateMusicVolume(volume);
    }

    private void ApplyMusicSettings()
    {
        bool isMusicEnabled = PlayerPrefs.GetInt(MusicEnabledKey, 1) == 1;
        musicToggle.isOn = isMusicEnabled;
        UpdateMusicState(isMusicEnabled);
    }

    private void UpdateSoundVolume(float volume)
    {
        GameObject[] soundObjects = GameObject.FindGameObjectsWithTag("Sound");
        foreach (GameObject soundObject in soundObjects)
        {
            AudioSource audioSource = soundObject.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.volume = volume;
            }
        }
    }

    private void UpdateMusicVolume(float volume)
    {
        GameObject[] musicObjects = GameObject.FindGameObjectsWithTag("BackgroundMusic");
        foreach (GameObject musicObject in musicObjects)
        {
            AudioSource audioSource = musicObject.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.volume = musicToggle.isOn ? volume : 0f;
            }
        }
    }

    private void UpdateMusicState(bool isMusicEnabled)
    {
        GameObject[] musicObjects = GameObject.FindGameObjectsWithTag("BackgroundMusic");
        foreach (GameObject musicObject in musicObjects)
        {
            AudioSource audioSource = musicObject.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.enabled = isMusicEnabled;
            }
        }
    }
}