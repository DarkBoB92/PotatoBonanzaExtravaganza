using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayAudio : MonoBehaviour
{
    public enum SoundFXCat { Shoot, Hit, Music, Spawn }
    public GameObject audioObject;

    public AudioClip Shoot;
    public AudioClip Hit;
    public AudioClip Music;
    public AudioClip Spawn;

    public void AudioTrigger(SoundFXCat audioType, Vector3 audioPosition, float volume)
    {
        GameObject newAudio = GameObject.Instantiate(audioObject, audioPosition, Quaternion.identity);
        MenuAudioManager mam = newAudio.GetComponent<MenuAudioManager>();

        switch (audioType)
        {
            case (SoundFXCat.Shoot):
                mam.myClip = Shoot;
                break;
            case (SoundFXCat.Hit):   // https://www.youtube.com/watch?v=rNvyG04GDCs
                mam.myClip = Hit;
                break;
            case (SoundFXCat.Music):
                mam.myClip = Music;
                break;
            case (SoundFXCat.Spawn):   // https://www.youtube.com/watch?v=CryJNQuyJsg
                mam.myClip = Spawn;
                break;
        }

        mam.volume = volume;
        mam.StartAudio();
    }
}