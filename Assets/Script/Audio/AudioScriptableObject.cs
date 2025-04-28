using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioScriptableObject", menuName = "ScriptableObjects/AudioScriptableObject")]
public class AudioScriptableObject : ScriptableObject
{
    public List<Sound> audioList;
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