using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Menu : MonoBehaviour
{
    [SerializeField] private UIDocument learnModal;

    private void Awake()
    {
        var returnButton = learnModal.rootVisualElement.Q<Button>("return");
        returnButton.clicked += HideModal;
        HideModal();
    }
    
    public void OnPlayButton() {
        SceneManager.LoadScene(1);
    }

    public void OnLearnButton()
    {
        learnModal.rootVisualElement.style.display = DisplayStyle.Flex;
    }

    public void HideModal()
    {
        learnModal.rootVisualElement.style.display = DisplayStyle.None;
    }
    
    public void OnQuitButton() {
        Application.Quit();
    }
}
