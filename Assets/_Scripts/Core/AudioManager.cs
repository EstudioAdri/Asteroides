using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] Sound[] sounds;
    void Awake()
    {
        foreach (Sound _sound in sounds)
        {
            _sound.source = gameObject.AddComponent<AudioSource>();
            _sound.source.clip = _sound.clip;

            _sound.source.volume = _sound.volume;
            _sound.source.pitch = _sound.pitch;
            _sound.source.loop = _sound.loop;
        }
    }

    public void Play(string name)
    {
        Sound _sound = Array.Find(sounds, sound => sound.name == name);
        if (_sound != null)
        {
            _sound.source.Play();
        }
    }
}
