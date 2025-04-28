using System.Collections.Generic;
using UnityEngine;

public class AudioService 
{
    private AudioSource audioSource;
    private AudioScriptableObject audioScriptableObject;
    private bool isMute;

    public AudioService(AudioSource audioSource , AudioScriptableObject audioScriptableObject)
    {
        this.audioSource = audioSource;
        this.audioScriptableObject = audioScriptableObject;
    }

    public void Play(SoundType soundType)
    {
        if (!isMute)
        {
            audioSource.PlayOneShot(GetSoundClip(soundType));
        }
    } 
    
    public void PlayClickSound()
    {
        if (!isMute)
        {
            Play(SoundType.Click);
        }
    }

    public AudioClip GetSoundClip(SoundType Stype)
    {
        Sound sound = audioScriptableObject.audioList.Find(x => x.soundType == Stype);
        return sound.soundClip;
    }

}

