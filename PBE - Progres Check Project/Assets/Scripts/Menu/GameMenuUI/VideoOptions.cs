using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class VideoOptions : MonoBehaviour
{
    public Toggle fullScreen, screenShake;
    public List<ResolutionValues> resolutions = new List<ResolutionValues>();
    public List<FontSize> fontSizes = new List<FontSize>();
    int selectedResolution, previousResolution, currentFontSize, previousFontSize;
    public TMP_Text resolutionText, fontSizeText;
    [SerializeField] GameUIManager gameUIManager;
    [SerializeField] GameObject popUpPanel;
    bool smallFontSize, mediumFontSize, bigFontSize, currentFullScreen, previousFullScreen;

    private void Start()
    {        
        popUpPanel.SetActive(false);
        fullScreen.isOn = Screen.fullScreen;
        currentFullScreen = fullScreen.isOn;
        previousFullScreen = fullScreen.isOn;

        bool foundResolution = false;
        for (int i = 0; i < resolutions.Count; i++)
        {
            if(Screen.width == resolutions[i].width && Screen.height == resolutions[i].height)
            {
                foundResolution = true;

                selectedResolution = i;
                previousResolution = selectedResolution;
                
                UpdateResolutionText();
            }
        }

        if (!foundResolution)
        {
            ResolutionValues newResolution = new ResolutionValues();
            newResolution.width = Screen.width;
            newResolution.height = Screen.height;

            resolutions.Add(newResolution);
            selectedResolution = resolutions.Count - 1;
            previousResolution = selectedResolution;

            UpdateResolutionText();
        }

        currentFontSize = 1;
        previousFontSize = currentFontSize;
        mediumFontSize = true;
        UpdateFontSizeText();
    }

    private void Update()
    {
        currentFullScreen = fullScreen.isOn;
    }

    public void ResolutionLeft()
    {
        selectedResolution--;
        if(selectedResolution < 0)
        {
            selectedResolution = resolutions.Count - 1;
        }

        UpdateResolutionText();
    }
    public void ResolutionRight()
    {        
        selectedResolution++;
        if(selectedResolution > resolutions.Count -1)
        {
            selectedResolution = 0;
        }
        UpdateResolutionText();
    }

    public void UpdateResolutionText()
    {
        resolutionText.text = resolutions[selectedResolution].width.ToString() + " x " + resolutions[selectedResolution].height.ToString();
    }

    public void FontSizeLeft()
    {
        currentFontSize--;
        if (currentFontSize < 0)
        {
            currentFontSize = fontSizes.Count - 1;
        }

        UpdateFontSizeText();
    }
    public void FontSizeRight()
    {
        currentFontSize++;
        if (currentFontSize > fontSizes.Count - 1)
        {
            currentFontSize = 0;
        }
        UpdateFontSizeText();
    }

    public void UpdateFontSizeText()
    {
        fontSizeText.text = fontSizes[currentFontSize].sizeText;
    }

    public void Apply()
    {
        if (gameUIManager.currentPanel != null)
        {
            Screen.SetResolution(resolutions[selectedResolution].width, resolutions[selectedResolution].height, fullScreen.isOn);
            fullScreen.isOn = currentFullScreen;
            previousFullScreen = currentFullScreen;
            previousResolution = selectedResolution;
            if(currentFontSize == 0 && !smallFontSize)
            {
                smallFontSize = true;
                if (mediumFontSize)
                {
                    foreach (TMP_Text text in gameUIManager.texts)
                    {
                        text.fontSize -= 10;
                        mediumFontSize = false;
                    }
                }
                else if (bigFontSize)
                {
                    foreach (TMP_Text text in gameUIManager.texts)
                    {
                        text.fontSize -= 20;
                        bigFontSize = false;
                    }
                }                                
            }
            else if(currentFontSize == 1 && !mediumFontSize)
            {
                mediumFontSize = true;
                if (smallFontSize)
                {
                    foreach (TMP_Text text in gameUIManager.texts)
                    {
                        text.fontSize += 10;
                        smallFontSize = false;
                    }
                }
                else if (bigFontSize)
                {
                    foreach (TMP_Text text in gameUIManager.texts)
                    {
                        text.fontSize -= 10;
                        bigFontSize = false;
                    }
                }
            }
            else if(currentFontSize == 2 && !bigFontSize)
            {
                bigFontSize = true;
                if (mediumFontSize)
                {
                    foreach (TMP_Text text in gameUIManager.texts)
                    {
                        text.fontSize += 10;
                        mediumFontSize = false;
                    }
                }
                else if (smallFontSize)
                {
                    foreach (TMP_Text text in gameUIManager.texts)
                    {
                        text.fontSize += 20;
                        smallFontSize = false;
                    }
                }
            }
            gameUIManager.videoSettingsScreen = false;
            gameUIManager.audioSettingsScreen = false;
            gameUIManager.controlSettingsScreen = false;
            gameUIManager.saveScreen = false;
            gameUIManager.videoSettingsPanel.SetActive(false);
            gameUIManager.audioSettingsPanel.SetActive(false);
            gameUIManager.controlSettingsPanel.SetActive(false);
            popUpPanel.SetActive(false);
            if (gameUIManager.player != null)
            {
                gameUIManager.ActivateCurrentButtons();
                fullScreen.enabled = true;
                screenShake.enabled = true;
                EventSystem.current.SetSelectedGameObject(null); //Clearing the current selected object
                if (gameUIManager.player.gamepad)
                {
                    EventSystem.current.SetSelectedGameObject(gameUIManager.firstSettingButton); //Setting a new current selected object
                }
            }
        }
    }

    public void Exit()
    {
        if (gameUIManager.currentPanel != null)
        {
            if (selectedResolution != previousResolution && !popUpPanel.activeInHierarchy || currentFontSize != previousFontSize && !popUpPanel.activeInHierarchy || currentFullScreen != previousFullScreen && !popUpPanel.activeInHierarchy) //<---And Changes Have Been Made
            {
                popUpPanel.SetActive(true);
                if (gameUIManager.player != null)
                {
                    gameUIManager.DeactivateCurrentButtons();
                    fullScreen.enabled = false;
                    screenShake.enabled = false;
                    EventSystem.current.SetSelectedGameObject(null); //Clearing the current selected object
                    if (gameUIManager.player.gamepad)
                    {
                        EventSystem.current.SetSelectedGameObject(gameUIManager.popUpButtons[0]); //Setting a new current selected object
                    }
                }
            }
            else
            {
                gameUIManager.videoSettingsScreen = false;
                gameUIManager.audioSettingsScreen = false;
                gameUIManager.controlSettingsScreen = false;
                gameUIManager.saveScreen = false;
                gameUIManager.videoSettingsPanel.SetActive(false);
                gameUIManager.audioSettingsPanel.SetActive(false);
                gameUIManager.controlSettingsPanel.SetActive(false);
                popUpPanel.SetActive(false);
                selectedResolution = previousResolution;
                currentFontSize = previousFontSize;
                fullScreen.isOn = previousFullScreen;
                UpdateResolutionText();
                if (gameUIManager.player != null)
                {
                    gameUIManager.ActivateCurrentButtons();
                    fullScreen.enabled = true;
                    screenShake.enabled = true;
                    EventSystem.current.SetSelectedGameObject(null); //Clearing the current selected object
                    if (gameUIManager.player.gamepad)
                    {
                        EventSystem.current.SetSelectedGameObject(gameUIManager.firstSettingButton); //Setting a new current selected object
                    }
                }
                
            }
        }
    }
}

[System.Serializable]
public class ResolutionValues
{
    public int width, height;
}

[System.Serializable]
public class FontSize
{
    public string sizeText;  
}
