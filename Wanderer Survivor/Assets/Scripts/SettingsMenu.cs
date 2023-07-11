using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource gameAudio;

    private void Start()
    {

        volumeSlider.onValueChanged.AddListener(UpdateVolume);
    }

    private void UpdateVolume(float value)
    {
        gameAudio.volume = value;
        Debug.Log(value);
    }
}