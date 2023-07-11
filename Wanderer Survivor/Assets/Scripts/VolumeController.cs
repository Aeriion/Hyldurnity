using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource gameAudio;

    private const string VolumeKey = "GameVolume";
    private const float MaxVolume = 1f;

    private void Start()
    {
        volumeSlider.minValue = 0f;
        volumeSlider.maxValue = MaxVolume;

        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, MaxVolume);
        volumeSlider.value = savedVolume;
        gameAudio.volume = savedVolume;

        volumeSlider.onValueChanged.AddListener(UpdateVolume);
    }

    private void UpdateVolume(float value)
    {
        gameAudio.volume = value;

        PlayerPrefs.SetFloat(VolumeKey, value);
        PlayerPrefs.Save();
    }
}