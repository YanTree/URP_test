using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    public bool loop;
    public bool play_on_awake;

    [Range(0f, 1f)]
    public float volume;

    [Range(.1f, 3f)]
    public float pitch;

    [Range(0, 256)]
    public int priority;

    [HideInInspector]
    public AudioSource source;
}