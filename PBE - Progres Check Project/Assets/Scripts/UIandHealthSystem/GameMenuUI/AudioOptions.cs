using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class AudioOptions : MonoBehaviour
{
    public TMP_Text musicVolumeText, effectsVolumeText;
    public Toggle mute;
    public Slider musicVolumeSlider, effectsVolumeSlider;
    float currentMusicVolume, currentEffectsVolume;
    bool currentMuteState;
    [SerializeField] GameUIManager gameUIManager;
    [SerializeField] GameObject popUpPanel;
    static float muted = -80;
    static float unMuted = 0;

    // Start is called before the first frame update
    void Start()
    {
        popUpPanel.SetActive(false);
        if(PlayerPrefs.GetFloat("MasterVolume") == muted)
        {
            mute.isOn = true;
        }
        else
        {
            mute.isOn = false;
        }
        SetMute();
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        effectsVolumeSlider.value = PlayerPrefs.GetFloat("EffectsVolume");
        currentMusicVolume = musicVolumeSlider.value;
        currentEffectsVolume = effectsVolumeSlider.value;
        UpdateMusicVolumeText();
        UpdateEffectsVolumeText();
    }

    public void SetMute()
    {
        if (mute.isOn)
        {
            gameUIManager.audioMixer.SetFloat("MasterVolume", -80f);
            
            PlayerPrefs.SetFloat("MasterVolume", muted);
        }
        else
        {
            gameUIManager.audioMixer.SetFloat("MasterVolume", 0f);
            
            PlayerPrefs.SetFloat("MasterVolume", unMuted);
        }
    }

    public void SetMusicVolume()
    {
        gameUIManager.audioMixer.SetFloat("MusicVolume", musicVolumeSlider.value);
        UpdateMusicVolumeText();
        
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
    }
    public void SetEffectsVolume()
    {        
        gameUIManager.audioMixer.SetFloat("EffectsVolume", effectsVolumeSlider.value);
        UpdateEffectsVolumeText();

        PlayerPrefs.SetFloat("EffectsVolume", effectsVolumeSlider.value);
    }

    public void UpdateMusicVolumeText()
    {
        musicVolumeText.text = (musicVolumeSlider.value + 100f).ToString();
    }
    public void UpdateEffectsVolumeText()
    {
        effectsVolumeText.text = (effectsVolumeSlider.value + 100f).ToString();
    }

    public void Apply()
    {
        currentMusicVolume = musicVolumeSlider.value;
        currentEffectsVolume = effectsVolumeSlider.value;
        currentMuteState = mute.isOn;
        Debug.Log("Your Settings Have Been Applied And Saved");
        gameUIManager.videoSettingsScreen = false;
        gameUIManager.audioSettingsScreen = false;
        gameUIManager.controlSettingsScreen = false;
        gameUIManager.saveScreen = false;
        gameUIManager.videoSettingsPanel.SetActive(false);
        gameUIManager.audioSettingsPanel.SetActive(false);
        gameUIManager.controlSettingsPanel.SetActive(false);
        popUpPanel.SetActive(false);
    }

    public void Exit()
    {
        if (gameUIManager.currentPanel != null)
        {
            if (musicVolumeSlider.value != currentMusicVolume && !popUpPanel.activeInHierarchy || effectsVolumeSlider.value != currentEffectsVolume && !popUpPanel.activeInHierarchy || mute.isOn != currentMuteState && !popUpPanel.activeInHierarchy) //<---And Changes Have Been Made
            {
                popUpPanel.SetActive(true);
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
                musicVolumeSlider.value = currentMusicVolume;
                effectsVolumeSlider.value = currentEffectsVolume;
                mute.isOn = currentMuteState;
                UpdateMusicVolumeText();
                UpdateEffectsVolumeText();
            }
        }
    }
}
