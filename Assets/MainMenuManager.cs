using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    
    [Header("Buttons")]
    [SerializeField] private Button NewGameButton;
    [SerializeField] private Button ContinueButton;
    [SerializeField] private Button SettingsButton;
    [SerializeField] private Button QuitButton;

    
    [Header("Panels")]
    [SerializeField] private GameObject MainMenuPanel;
    [SerializeField] private GameObject NewGamePanel;
    [SerializeField] private GameObject ContinuePanel;
    [SerializeField] private GameObject SettingsPanel;
    
    private void Start()
    {
        NewGameButton.onClick.AddListener(OpenNewGamePanel);
        ContinueButton.onClick.AddListener(OpenContinuePanel);
        SettingsButton.onClick.AddListener(OpenSettingsPanel);
        QuitButton.onClick.AddListener(QuitGame);
    }
    
    
    private void OpenNewGamePanel()
    {
        
        //MainMenuPanel.SetActive(false);
        //NewGamePanel.SetActive(true);
        LoadScreenManager.instance.LoadScene("InitialArea");
    }
    
    private void OpenContinuePanel()
    {
        MainMenuPanel.SetActive(false);
        ContinuePanel.SetActive(true);
    }
    
    
    private void OpenSettingsPanel()
    {
        MainMenuPanel.SetActive(false);
        SettingsPanel.SetActive(true);
    }
    
    public void ClosePannel(GameObject panel)
    {
        panel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }
    
    private void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
