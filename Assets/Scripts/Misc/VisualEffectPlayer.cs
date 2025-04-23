using System.Collections;
using System.Collections.Generic;
using UnityEngine.VFX;
using UnityEngine;

public class VisualEffectPlayer : MonoBehaviour
{
    [SerializeField] private VisualEffect visualEffect;

    private bool _isPlaying;
    private bool _isWaitingForParticles;


    private void Update()
    {
        if (_isWaitingForParticles && visualEffect.aliveParticleCount > 0)
        {
            _isWaitingForParticles = false;
            _isPlaying = true;
        }

        if (_isPlaying && visualEffect.aliveParticleCount == 0)
        {
            OnFinishedPlaying();
        }
    }

    public void Play()
    {
        visualEffect.enabled = true;
        visualEffect.Play();
        _isWaitingForParticles = true;
    }

    private void OnFinishedPlaying()
    {
        Destroy(this.gameObject);
    }
}
