using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClipsListSO audioClipsList;
    [SerializeField] private AudioClipSettings defaultAudioOST;


    public void PlayDefaultOST()
    {
        PlayClip(defaultAudioOST.ID, (defaultAudioOST.attachTransform) ? transform : null, defaultAudioOST.looped, defaultAudioOST.volume, defaultAudioOST.spatialBlend);
    }

    private AudioClipData GetAudioData(AudioID audioID)
    {
        for (int i = 0; i < audioClipsList.List.Count; i++)
        {
            if (audioClipsList.List[i].ID == audioID)
            {
                return audioClipsList.List[i];
            }
        }
        return null;
    }

    public void PlayClip(AudioID audioID, Transform parent = null, bool looped = false, float volume = 1f, float spatialBlend = 0.4f)
    {
        AudioClipData audioData = GetAudioData(audioID);
        if (audioData == null)
        {
            return;
        }

        AudioPlayer audioPlayer = GameManager.Instance.ObjectPoolingManager.GetAudioPlayer();
        if (parent != null)
        {
            audioPlayer.transform.parent = parent;
            audioPlayer.transform.position = parent.position;
        }

        if (looped)
        {
            audioPlayer.PlayLooped(audioData.audioClip, audioData.mixerGroup, volume, spatialBlend);
        }
        else
        {
            audioPlayer.Play(audioData.audioClip, audioData.mixerGroup, volume, spatialBlend);
        }
    }
}