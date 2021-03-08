﻿using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.loop = s.loop;
            s.source.playOnAwake = s.play_on_awake;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.priority = s.priority;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Play Sound: " + name + "Not Found!!!");
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Stop Sound: " + name + "Not Found!!!");
            return;
        }
        s.source.Stop();
    }

    public void PlaySound(string name, bool shouldplay)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Stop Sound: " + name + "Not Found!!!");
            return;
        }
        Debug.Log("IsPlay: " + shouldplay);
        if (shouldplay)
        {
            if(!s.source.isPlaying)
                s.source.Play();
        }
        else
            s.source.Stop();
    }
}