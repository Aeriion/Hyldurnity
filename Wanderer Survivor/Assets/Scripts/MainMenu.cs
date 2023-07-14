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

    public GameObject buttonSoundObject; // GameObject contenant l'AudioSource

    private AudioSource buttonAudioSource; // Référence à l'AudioSource

    private void Start()
    {
        playButton.onClick.AddListener(PlayGame);
        optionsButton.onClick.AddListener(OpenOptionsPanel);
        bestiaryButton.onClick.AddListener(OpenBestiaryCanvas);
        breedingButton.onClick.AddListener(OpenBreedingCanvas);
        quitButton.onClick.AddListener(QuitGame);

        // Récupérer l'AudioSource du GameObject
        buttonAudioSource = buttonSoundObject.GetComponent<AudioSource>();

        // Ajout de l'écouteur de clic pour jouer le son du bouton
        playButton.onClick.AddListener(PlayButtonSound);
        optionsButton.onClick.AddListener(PlayButtonSound);
        bestiaryButton.onClick.AddListener(PlayButtonSound);
        breedingButton.onClick.AddListener(PlayButtonSound);
        quitButton.onClick.AddListener(PlayButtonSound);
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

    private void PlayButtonSound()
    {
        buttonAudioSource.Play(); // Jouer le son du bouton
    }
}