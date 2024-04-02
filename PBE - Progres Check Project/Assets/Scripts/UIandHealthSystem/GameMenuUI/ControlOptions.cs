using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControlOptions : MonoBehaviour
{
    public Toggle mouseKeyboardRH, mouseKeyboardLH, joypad;
    bool currentStateMouseKeyboardRH, currentStateMouseKeyboardLH, currentStateJoypad;
    [SerializeField] GameUIManager gameUIManager;
    [SerializeField] GameObject popUpPanel;

    private void Start()
    {
        mouseKeyboardRH.isOn = true;
        mouseKeyboardLH.isOn = false;
        joypad.isOn = false;
        currentStateMouseKeyboardRH = mouseKeyboardRH.isOn;
        currentStateMouseKeyboardLH = mouseKeyboardLH.isOn;
        currentStateJoypad = joypad.isOn;
    }

    public void SetMouseKeyboardRH()
    {
        if (mouseKeyboardRH.isOn)
        {            
            mouseKeyboardLH.isOn = false;
            joypad.isOn = false;
        }
    }
    public void SetMouseKeyboardLH()
    {
        if (mouseKeyboardLH.isOn)
        {            
            mouseKeyboardRH.isOn = false;
            joypad.isOn = false;
        }
    }
    public void SetJoypad()
    {
        if (joypad.isOn)
        {            
            mouseKeyboardRH.isOn = false;
            mouseKeyboardLH.isOn = false;
        }
    }

    public void Apply()
    {
        if (!mouseKeyboardRH.isOn && !mouseKeyboardLH.isOn && !joypad.isOn)
        {
            mouseKeyboardRH.isOn = currentStateMouseKeyboardRH;
            mouseKeyboardLH.isOn = currentStateMouseKeyboardLH;
            joypad.isOn = currentStateJoypad;
        }
        else
        {
            currentStateMouseKeyboardRH = mouseKeyboardRH.isOn;
            currentStateMouseKeyboardLH = mouseKeyboardLH.isOn;
            currentStateJoypad = joypad.isOn;
        }
        gameUIManager.videoSettingsScreen = false;
        gameUIManager.audioSettingsScreen = false;
        gameUIManager.controlSettingsScreen = false;
        gameUIManager.saveScreen = false;
        gameUIManager.videoSettingsPanel.SetActive(false);
        gameUIManager.audioSettingsPanel.SetActive(false);
        gameUIManager.controlSettingsPanel.SetActive(false);
        popUpPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null); //Clearing the current selected object
        if (gameUIManager.player != null)
        {
            gameUIManager.ActivateCurrentButtons();
            mouseKeyboardRH.enabled = true;
            mouseKeyboardLH.enabled = true;
            joypad.enabled = true;
            EventSystem.current.SetSelectedGameObject(null); //Clearing the current selected object
            Cursor.visible = true;
            if (gameUIManager.player.gamepad)
            {
                Cursor.visible = false;
                EventSystem.current.SetSelectedGameObject(gameUIManager.firstSettingButton); //Setting a new current selected object
            }
        }
    }

    public void Exit()
    {
        if (gameUIManager.currentPanel != null)
        {
            if (mouseKeyboardRH.isOn != currentStateMouseKeyboardRH && !popUpPanel.activeInHierarchy || mouseKeyboardLH.isOn != currentStateMouseKeyboardLH && !popUpPanel.activeInHierarchy) //<---And Changes Have Been Made
            {
                popUpPanel.SetActive(true);
                if (gameUIManager.player != null)
                {
                    gameUIManager.DeactivateCurrentButtons();
                    mouseKeyboardRH.enabled = false;
                    mouseKeyboardLH.enabled = false;
                    joypad.enabled = false;
                    EventSystem.current.SetSelectedGameObject(null); //Clearing the current selected object
                    Cursor.visible = true;
                    if (gameUIManager.player.gamepad)
                    {
                        Cursor.visible = false;
                        EventSystem.current.SetSelectedGameObject(gameUIManager.popUpButtons[4]); //Setting a new current selected object
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
                mouseKeyboardRH.isOn = currentStateMouseKeyboardRH;
                mouseKeyboardLH.isOn = currentStateMouseKeyboardLH;
                joypad.isOn = currentStateJoypad;
                if (gameUIManager.player != null)
                {
                    gameUIManager.ActivateCurrentButtons();
                    mouseKeyboardRH.enabled = true;
                    mouseKeyboardLH.enabled = true;
                    joypad.enabled = true;
                    Cursor.visible = true;
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
