using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private UIDocument pauseMenu;
    private bool isPaused;

    public bool GetIsPaused()
    {
        return isPaused;
    }

    private void Awake()
    {
        var resumeButton = pauseMenu.rootVisualElement.Q<Button>("resume");
        var restartButton = pauseMenu.rootVisualElement.Q<Button>("restart");
        var quitButton = pauseMenu.rootVisualElement.Q<Button>("quit");
        
        resumeButton.clicked += Resume;
        restartButton.clicked += Restart;
        quitButton.clicked += Quit;

        Time.timeScale = 1;
        isPaused = false;
        pauseMenu.rootVisualElement.style.display = DisplayStyle.None;
    }
    
    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) {
            return;
        }
        if (Time.timeScale is 1) {
            Pause();
        } else {
            Resume();
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
    
    private void Restart() {
        Time.timeScale = 1;
        isPaused = false;
        pauseMenu.rootVisualElement.style.display = DisplayStyle.None;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
