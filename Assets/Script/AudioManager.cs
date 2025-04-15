using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSFX;
    private static AudioManager instance;
    [SerializeField] private List<Sound> sounds = new List<Sound>();
    public static AudioManager Instance { get { return instance; } }
    [SerializeField] private bool isMute;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

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