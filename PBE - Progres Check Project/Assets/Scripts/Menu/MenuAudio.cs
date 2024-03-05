using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudio : MonoBehaviour
{
    public enum SoundFXCat { LogoWoosh, CameraWoosh, FridgeHum, MouseHover, ButtonSelect, ButtonBack, KitchenSFX, Jazz }
    public GameObject audioObject;

    public AudioClip LogoWoosh;
    public AudioClip CameraWoosh;
    public AudioClip FridgeHum;
    public AudioClip MouseHover;
    public AudioClip ButtonSelect;
    public AudioClip ButtonBack;
    public AudioClip KitchenSFX;
    public AudioClip Jazz;


    public void AudioTrigger(SoundFXCat audioType, Vector3 audioPosition, float volume)
    {
        GameObject newAudio = GameObject.Instantiate(audioObject, audioPosition, Quaternion.identity);
        MenuAudioManager mam = newAudio.GetComponent<MenuAudioManager>();

        switch (audioType)
        {
            case (SoundFXCat.LogoWoosh):
                mam.myClip = LogoWoosh;
                break;
            case (SoundFXCat.CameraWoosh):
                mam.myClip = CameraWoosh;
                break;
            case (SoundFXCat.FridgeHum):
                mam.myClip = FridgeHum;
                break;
            case (SoundFXCat.MouseHover):
                mam.myClip = MouseHover;
                break;
            case (SoundFXCat.ButtonSelect):
                mam.myClip = ButtonSelect;
                break;
            case (SoundFXCat.ButtonBack):
                mam.myClip = ButtonBack;
                break;
            case (SoundFXCat.KitchenSFX):
                mam.myClip = KitchenSFX;
                break;
            case (SoundFXCat.Jazz):
                mam.myClip = Jazz;
                break;
        }

        mam.volume = volume;
        mam.StartAudio();
    }
}