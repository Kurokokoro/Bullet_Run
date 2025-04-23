using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioClipsList", menuName = "ScriptableObjects/AudioClipsList")]
public class AudioClipsListSO : ScriptableObject
{
    public List<AudioClipData> List;
}

[System.Serializable]
public class AudioClipData
{
    public AudioID ID;
    public UnityEngine.Audio.AudioMixerGroup mixerGroup;
    public AudioClip audioClip;
}

[System.Serializable]
public class AudioClipSettings
{
    public AudioID ID;
    public bool looped;
    public bool attachTransform;
    public float volume;
    public float spatialBlend;
}

public enum AudioID
{
    None            = 0,
    OST             = 1,
    Hit_01          = 2,
    Hit_Glass       = 3,
    Hit_Soft        = 4,
}