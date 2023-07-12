using UnityEngine;
using UnityEngine.UI;

public class CloseCanvasButton : MonoBehaviour
{
    public Button closeButton;

    private void Start()
    {
        closeButton.onClick.AddListener(CloseCurrentCanvas);
    }

    private void CloseCurrentCanvas()
    {
        Canvas currentCanvas = GetComponentInParent<Canvas>();
        currentCanvas.gameObject.SetActive(false);
    }
}