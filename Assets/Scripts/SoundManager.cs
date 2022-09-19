using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource, _effectSource;

    private Vector2 pitch = Vector2.one;

    public static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<SoundManager>();
            return instance;
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip == null) return;
        _effectSource.pitch = Random.Range(0.6f, 1.1f);
        _effectSource.PlayOneShot(clip);
    }

    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }
}
