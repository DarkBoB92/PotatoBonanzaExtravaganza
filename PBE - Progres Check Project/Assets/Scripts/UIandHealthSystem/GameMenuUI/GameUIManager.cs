using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class GameUIManager : MonoBehaviour
{
    public enum GameState { MainMenu, Paused, Playing, GameOver };
    public GameState currentState;
    public GameObject allGameUI, pauseMenuPanel, saveFilePanel, settingsPanel, videoSettingsPanel, audioSettingsPanel, controlSettingsPanel, gameOverPanel, titleText, currentPanel;
    public bool saveScreen, settingsScreen, videoSettingsScreen, audioSettingsScreen, controlSettingsScreen, playerIsDead;
    public TMP_Text[] texts;
    public AudioMixer audioMixer;
    public Button currentButton;
    public Button[] buttons;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "PrototypeMenu")
        {
            CheckGameState(GameState.MainMenu);
        }
        else
        {
            CheckGameState(GameState.Playing);
        }

        texts = GetComponentsInChildren<TMP_Text>(true);       
        buttons = GetComponentsInChildren<Button>(false);
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
        titleText.SetActive(false);
    }

    public void GamePaused()
    {
        allGameUI.SetActive(true);
        pauseMenuPanel.SetActive(true);
        saveFilePanel.SetActive(false);
        settingsPanel.SetActive(false);
        videoSettingsPanel.SetActive(false);
        audioSettingsPanel.SetActive(false);
        controlSettingsPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        titleText.SetActive(true);
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
        gameOverPanel.SetActive(true);
    }


    void Update()
    {
        CheckInputs();
    }

    void CheckInputs()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Playing)
            {
                CheckGameState(GameState.Paused);
                buttons = GetComponentsInChildren<Button>(false);
            }
            else if (currentState == GameState.Paused)
            {
                CheckGameState(GameState.Playing);
            }
        }
    }    

    public void StartGame()
    {
        SceneManager.LoadScene("GameStates");
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
        }
        else
        {
            videoSettingsPanel.SetActive(false);
            videoSettingsScreen = false;
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
        }
        else
        {
            audioSettingsPanel.SetActive(false);
            audioSettingsScreen = false;
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
        }
        else
        {
            controlSettingsPanel.SetActive(false);
            controlSettingsScreen = false;
        }
    }

    public void Apply()
    {
        if (currentPanel != null)
        {
            if (currentPanel == settingsPanel) //<---And Changes Have Been Made, if No changes have been made, just closes the settings
            {
                Debug.Log("Your Settings Have Been Applied And Saved");
            }
            currentPanel.SetActive(false);
            currentPanel = null;
            videoSettingsPanel.SetActive(false);
            audioSettingsPanel.SetActive(false);
            controlSettingsPanel.SetActive(false);
            videoSettingsScreen = false;
            audioSettingsScreen = false;
            controlSettingsScreen = false;
            settingsScreen = false;
            saveScreen = false;
        }
    }

    public void Exit()
    {
        if (currentPanel != null)
        {            
            {
                currentPanel.SetActive(false);
                currentPanel = null;
                videoSettingsPanel.SetActive(false);
                audioSettingsPanel.SetActive(false);
                controlSettingsPanel.SetActive(false);
                settingsScreen = false;
                saveScreen = false;
                foreach (Button button in buttons)
                {
                    button.interactable = true;
                }
            }
        }
    }

    public void ResumeGame()
    {
        CheckGameState(GameState.Playing);
    }

    public void GameSaved()
    {
        Debug.Log("Game Saved!");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
        CheckGameState(GameState.MainMenu);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
