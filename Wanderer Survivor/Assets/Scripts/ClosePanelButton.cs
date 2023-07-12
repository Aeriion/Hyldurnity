using UnityEngine;
using UnityEngine.UI;

public class ClosePanelButton : MonoBehaviour
{
    public Button closeButton;
    public GameObject panel;

    private void Start()
    {
        closeButton.onClick.AddListener(ClosePanel);
    }

    private void ClosePanel()
    {
        panel.SetActive(false);
    }
}