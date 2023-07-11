using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public Button optionsButton;
    public Button bestiaryButton;
    public Button breedingButton;
    public Button quitButton;

    public GameObject optionsPanel;
    public GameObject bestiaryCanvas;
    public GameObject breedingCanvas;

    private void Start()
    {
        playButton.onClick.AddListener(PlayGame);
        optionsButton.onClick.AddListener(OpenOptionsPanel);
        bestiaryButton.onClick.AddListener(OpenBestiaryCanvas);
        breedingButton.onClick.AddListener(OpenBreedingCanvas);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void OpenOptionsPanel()
    {
        optionsPanel.SetActive(true);
    }

    private void OpenBestiaryCanvas()
    {
        bestiaryCanvas.SetActive(true);
    }

    private void OpenBreedingCanvas()
    {
        breedingCanvas.SetActive(true);
    }

    private void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}