using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private AudioClip _audioClip;

    public bool IsPlaying => audioSource.isPlaying;


    private void Update()
    {
        if (audioSource.isPlaying == false)
        {
            OnClipEnd();
        }
    }

    public void Play(AudioClip clip, UnityEngine.Audio.AudioMixerGroup mixerGroup, float volume, float spatialBlend)
    {
        gameObject.SetActive(true);
        _audioClip = clip;
        audioSource.clip = _audioClip;
        audioSource.time = 0f;
        audioSource.volume = volume;
        audioSource.spatialBlend = spatialBlend;
        audioSource.outputAudioMixerGroup = mixerGroup;
        audioSource.Play();
    }

    public void PlayLooped(AudioClip clip, UnityEngine.Audio.AudioMixerGroup mixerGroup, float volume, float spatialBlend)
    {
        audioSource.loop = true;
        Play(clip, mixerGroup, volume, spatialBlend);
    }

    public void Stop()
    {
        audioSource.Stop();
    }

    public void OnClipEnd()
    {
        GameManager.Instance.ObjectPoolingManager.ReturnAudioPlayer(this);
    }
}