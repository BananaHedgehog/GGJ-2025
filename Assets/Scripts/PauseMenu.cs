using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private UIDocument pauseMenu;
    [SerializeField] private UIDocument learnModal;
    private bool isPaused;

    public bool GetIsPaused()
    {
        return isPaused;
    }

    private void Awake()
    {
        pauseMenu.rootVisualElement.style.display = DisplayStyle.None;
        learnModal.rootVisualElement.style.display = DisplayStyle.None;

        var resumeButton = pauseMenu.rootVisualElement.Q<Button>("resume");
        var restartButton = pauseMenu.rootVisualElement.Q<Button>("restart");
        var learnButton = pauseMenu.rootVisualElement.Q<Button>("learn");
        var quitButton = pauseMenu.rootVisualElement.Q<Button>("quit");

        resumeButton.clicked += Resume;
        restartButton.clicked += Restart;
        learnButton.clicked += ShowHelp;
        quitButton.clicked += Quit;

        Time.timeScale = 1;
        isPaused = false;

        var returnButton = learnModal.rootVisualElement.Q<Button>("return");
        returnButton.clicked += () => learnModal.rootVisualElement.style.display = DisplayStyle.None;
    }
    
    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) {
            return;
        }

        if (PlayerController.IsDead())
        {
            return;   
        }
        if (Time.timeScale is 1) {
            Pause();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        } else {
            Resume();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void Pause()
    {
        Time.timeScale = 0;
        isPaused = true;
        pauseMenu.rootVisualElement.style.display = DisplayStyle.Flex;
    }
    
    private void Resume() {
        Time.timeScale = 1;
        isPaused = false;
        pauseMenu.rootVisualElement.style.display = DisplayStyle.None;
    }
    
    private void Quit() {
        Application.Quit();
    }

    private void ShowHelp()
    {
        pauseMenu.rootVisualElement.style.display = DisplayStyle.None;
        learnModal.rootVisualElement.style.display = DisplayStyle.Flex;
    }
    
    private void Restart() {
        Time.timeScale = 1;
        isPaused = false;
        pauseMenu.rootVisualElement.style.display = DisplayStyle.None;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
