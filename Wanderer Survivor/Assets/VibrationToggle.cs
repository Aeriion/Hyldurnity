using UnityEngine;
using UnityEngine.UI;

public class VibrationToggle : MonoBehaviour
{
    public Toggle vibrationToggle;

    private void Start()
    {
        // Vérifier l'état actuel de la vibration et mettre à jour l'état du Toggle en conséquence
        vibrationToggle.isOn = PlayerPrefs.GetInt("VibrationEnabled", 1) == 1;
    }

    public void ToggleVibration()
    {
        // Activer ou désactiver les vibrations en fonction de l'état du Toggle
        if (vibrationToggle.isOn)
        {
            PlayerPrefs.SetInt("VibrationEnabled", 1);
        }
        else
        {
            PlayerPrefs.SetInt("VibrationEnabled", 0);
        }
    }
}