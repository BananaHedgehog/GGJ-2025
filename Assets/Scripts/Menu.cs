using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Menu : MonoBehaviour
{
    [SerializeField] private UIDocument mainMenu;
    [SerializeField] private UIDocument learnModal;

    private void Awake()
    {
        mainMenu.rootVisualElement.style.display = DisplayStyle.Flex;
        mainMenu.rootVisualElement.Q<Button>("start").clicked += OnPlayButton;
        mainMenu.rootVisualElement.Q<Button>("learn").clicked += OnLearnButton;
        mainMenu.rootVisualElement.Q<Button>("quit").clicked += OnQuitButton;
        
        var returnButton = learnModal.rootVisualElement.Q<Button>("return");
        returnButton.clicked += HideModal;
        HideModal();
    }
    
    private void OnPlayButton() {
        mainMenu.rootVisualElement.style.display = DisplayStyle.None;
        SceneManager.LoadScene(1);
    }

    private void OnLearnButton()
    {
        mainMenu.rootVisualElement.style.display = DisplayStyle.None;
        learnModal.rootVisualElement.style.display = DisplayStyle.Flex;
    }

    private void HideModal()
    {
        learnModal.rootVisualElement.style.display = DisplayStyle.None;
        mainMenu.rootVisualElement.style.display = DisplayStyle.Flex;
    }
    
    private void OnQuitButton() {
        Application.Quit();
    }
}
