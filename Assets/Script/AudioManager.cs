using System.Collections.Generic;
using UnityEngine;

public class AudioManager : GenericMonoSingleton<AudioManager>
{
    [SerializeField] private AudioSource audioSFX;
    [SerializeField] private List<Sound> sounds = new List<Sound>();
    [SerializeField] private bool isMute;

    public void Play(SoundType soundType)
    {
        if (!isMute)
        {
            audioSFX.PlayOneShot(GetSoundClip(soundType));
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
        Sound sound = sounds.Find(x => x.soundType == Stype);
        return sound.soundClip;
    }

}

[System.Serializable]
public class Sound
{
    public SoundType soundType;
    public AudioClip soundClip;
}

public enum SoundType
{
    Click,
    SelectSlot,
    AddResource,
    BuySuccess,
    SellSuccess,
    BuyFailed,
}