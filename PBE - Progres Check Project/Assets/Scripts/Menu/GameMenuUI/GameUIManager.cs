using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using System.Linq;

public class GameUIManager : MonoBehaviour
{
    public enum GameState { MainMenu, Paused, Playing, GameOver };
    public GameState currentState;
    public GameObject allGameUI, pauseMenuPanel, saveFilePanel, settingsPanel, videoSettingsPanel, audioSettingsPanel, controlSettingsPanel, gameOverPanel, titleText, currentPanel;
    public bool saveScreen, settingsScreen, videoSettingsScreen, audioSettingsScreen, controlSettingsScreen, playerIsDead;
    public TMP_Text[] texts;
    public AudioMixer audioMixer;
    public Button currentButton;
    public Button[] buttons, menuButtons;
    TutorialControlsScript tutorialUI;
    ControlOptions controlPanel;
    public GameObject firstSettingButton, firstSaveButton; 
    public GameObject[] popUpButtons;
    public NewPlayerController player;

    private void Awake()
    {
        Time.timeScale = 1f;

        if (SceneManager.GetActiveScene().name == "Menu")
        {
            CheckGameState(GameState.MainMenu);
        }
        else
        {
            CheckGameState(GameState.Playing);
        }

        controlPanel = GetComponentInChildren<ControlOptions>(true);
        texts = GetComponentsInChildren<TMP_Text>(true);
        buttons = GetComponentsInChildren<Button>(false);        

        tutorialUI = GameObject.FindGameObjectWithTag("Canvas").GetComponent<TutorialControlsScript>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<NewPlayerController>();
    }

    void PopulateMenuButtons()
    {
        menuButtons = buttons.ToArray();
    }

    public void CheckGameState(GameState newGameState)
    {
        currentState = newGameState;
        switch (currentState)
        {
            case GameState.MainMenu:
                MainMenuSetup();
                Time.timeScale = 1f;
                break;
            case GameState.Paused:
                GamePaused();
                Time.timeScale = 0f;                
                break;
            case GameState.Playing:
                GameActive();
                Time.timeScale = 1f;
                break;
            case GameState.GameOver:
                GameOver();
                Time.timeScale = 0f;
                break;
        }
    }

    public void MainMenuSetup()
    {
        allGameUI.SetActive(false);
        pauseMenuPanel.SetActive(false);
        saveFilePanel.SetActive(false);
        settingsPanel.SetActive(false);
        videoSettingsPanel.SetActive(false);
        audioSettingsPanel.SetActive(false);
        controlSettingsPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    public void GameActive()
    {
        allGameUI.SetActive(true);
        pauseMenuPanel.SetActive(false);
        saveFilePanel.SetActive(false);
        settingsPanel.SetActive(false);
        videoSettingsPanel.SetActive(false);
        audioSettingsPanel.SetActive(false);
        controlSettingsPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    public void GamePaused()
    {
        allGameUI.SetActive(false);
        pauseMenuPanel.SetActive(true);
        saveFilePanel.SetActive(false);
        settingsPanel.SetActive(false);
        videoSettingsPanel.SetActive(false);
        audioSettingsPanel.SetActive(false);
        controlSettingsPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    public void GameOver()
    {
        allGameUI.SetActive(false);
        pauseMenuPanel.SetActive(false);
        saveFilePanel.SetActive(false);
        settingsPanel.SetActive(false);
        videoSettingsPanel.SetActive(false);
        audioSettingsPanel.SetActive(false);
        controlSettingsPanel.SetActive(false);
        gameOverPanel.SetActive(true); GetCurrentButtons();
    }

    public void CheckInputs(bool pressed)
    {
        if (tutorialUI != null && tutorialUI.firstPlay)
        {
            if (pressed)
            {
                if (currentState == GameState.Playing)
                {
                    CheckGameState(GameState.Paused);
                    buttons = GetComponentsInChildren<Button>(false);
                    PopulateMenuButtons();
                    if (player != null && player.gamepad)
                    {
                        Cursor.visible = false;
                        EventSystem.current.SetSelectedGameObject(null); //Clearing the current selected object
                        EventSystem.current.SetSelectedGameObject(buttons[0].gameObject); //Setting a new current selected object
                    }
                }
                else if (currentState == GameState.Paused)
                {
                    if (saveScreen)
                    {
                        if (player != null && player.gamepad)
                        {
                            Cursor.visible = true;
                            EventSystem.current.SetSelectedGameObject(null); //Clearing the current selected object
                            EventSystem.current.SetSelectedGameObject(buttons[1].gameObject); //Setting a new current selected object
                        }
                    }
                    else if (settingsScreen)
                    {
                        if (player != null && player.gamepad)
                        {
                            EventSystem.current.SetSelectedGameObject(null); //Clearing the current selected object
                            EventSystem.current.SetSelectedGameObject(buttons[0].gameObject); //Setting a new current selected object
                        }
                    }
                    else
                    {
                        CheckGameState(GameState.Playing);
                    }
                }
            }
        }
    }    

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
        currentState = GameState.Playing;
    }

    public void PauseGame()
    {
        CheckGameState(GameState.Paused);
    }

    public void SaveGame()
    {
        if (!saveScreen)
        {
            saveFilePanel.SetActive(true);
            settingsPanel.SetActive(false);
            settingsScreen = false;
            saveScreen = true;
            currentPanel = saveFilePanel;
            currentButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
            foreach (Button button in buttons)
            {
                button.interactable = false;
                currentButton.interactable = true;                
            }
            EventSystem.current.SetSelectedGameObject(null); //Clearing the current selected object
            if (player != null && player.gamepad)
            {
                EventSystem.current.SetSelectedGameObject(firstSaveButton); //Setting a new current selected object
            }
        }
        else
        {
            saveFilePanel.SetActive(false);
            saveScreen = false;
            currentPanel = null;
            foreach (Button button in buttons)
            {
                button.interactable = true;
                currentButton = null;
            }
            if (!player.gamepad)
            {
                EventSystem.current.SetSelectedGameObject(null); //Clearing the current selected object
            }
        }
    }

    public void Settings()
    {
        if (!settingsScreen)
        {
            settingsPanel.SetActive(true);
            saveFilePanel.SetActive(false);
            settingsScreen = true;
            saveScreen = false;
            currentPanel = settingsPanel;
            currentButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
            foreach (Button button in buttons)
            {
                button.interactable = false;
                currentButton.interactable = true;
            }
            GetCurrentButtons();
            EventSystem.current.SetSelectedGameObject(null); //Clearing the current selected object
            if (player != null && player.gamepad)
            {
                EventSystem.current.SetSelectedGameObject(firstSettingButton); //Setting a new current selected object
            }
        }
        else
        {
            settingsPanel.SetActive(false);
            settingsScreen = false;
            videoSettingsPanel.SetActive(false);
            audioSettingsPanel.SetActive(false);
            controlSettingsPanel.SetActive(false);
            videoSettingsScreen = false;
            audioSettingsScreen = false;
            controlSettingsScreen = false;
            currentPanel = null;
            GetCurrentButtons();
            foreach (Button button in buttons)
            {
                button.interactable = true;
                currentButton = null;
            }
        }
    }

    public void VideoSettings()
    {
        if (!videoSettingsScreen)
        {
            videoSettingsPanel.SetActive(true);
            audioSettingsPanel.SetActive(false);
            controlSettingsPanel.SetActive(false);
            videoSettingsScreen = true;
            audioSettingsScreen = false;
            controlSettingsScreen = false;
            GetCurrentButtons();
        }
        else
        {
            videoSettingsPanel.SetActive(false);
            videoSettingsScreen = false;
            GetCurrentButtons();
        }
    }

    public void AudioSettings()
    {
        if (!audioSettingsScreen)
        {
            audioSettingsPanel.SetActive(true);
            videoSettingsPanel.SetActive(false);
            controlSettingsPanel.SetActive(false);
            videoSettingsScreen = false;
            audioSettingsScreen = true;
            controlSettingsScreen = false;
            GetCurrentButtons();
        }
        else
        {
            audioSettingsPanel.SetActive(false);
            audioSettingsScreen = false;
            GetCurrentButtons();
        }
    }

    public void ControlSettings()
    {
        if (!controlSettingsScreen)
        {
            controlSettingsPanel.SetActive(true);
            videoSettingsPanel.SetActive(false);
            audioSettingsPanel.SetActive(false);
            videoSettingsScreen = false;
            audioSettingsScreen = false;
            controlSettingsScreen = true;
            GetCurrentButtons();
        }
        else
        {
            controlSettingsPanel.SetActive(false);
            controlSettingsScreen = false;
            GetCurrentButtons();
        }
    }

    public void GetCurrentButtons()
    {
        buttons = GetComponentsInChildren<Button>(false);
        if (menuButtons.Length < 0)
        {
            PopulateMenuButtons();
        }        
        if (!player.gamepad)
        {
            EventSystem.current.SetSelectedGameObject(null); //Clearing the current selected object
        }
    }
    public void ActivateCurrentButtons()
    {
        if (settingsScreen)
        {
            foreach (Button button in buttons)
            {
                button.interactable = true;
                foreach (Button menuButton in menuButtons)
                {
                    menuButton.interactable = false;
                    currentButton.interactable = true;
                }
            }            
        }
        else
        {
            foreach (Button button in buttons)
            {
                button.interactable = true;
            }
        }
    }

    public void DeactivateCurrentButtons()
    {
        foreach (Button button in buttons)
            {
                button.interactable = false;
            }
    }

    public void ResumeGame()
    {
        CheckGameState(GameState.Playing);
        if (player != null && player.gamepad)
        {
            Cursor.visible = true;
            EventSystem.current.SetSelectedGameObject(null); //Clearing the current selected object
        }
    }

    public void GameSaved()
    {
        Debug.Log("Game Saved!");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
        CheckGameState(GameState.MainMenu);
        Cursor.visible = true;
        if (player != null && player.gamepad)
        {            
            EventSystem.current.SetSelectedGameObject(buttons[1].gameObject); //Setting a new current selected object
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
