/**
 * Tutorial: https://www.youtube.com/watch?v=6OT43pvUyfY
 */

using System;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager Instance;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        
        // To don't restart music on change level.
        DontDestroyOnLoad(gameObject);
        
        foreach (Sound sound in sounds)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.AudioClip;

            sound.Source.volume = sound.Volume;
            sound.Source.pitch = sound.Pitch;
            sound.Source.loop = sound.Loop;
        }
    }

    public void Play(string name)
    {
        Sound sound = GetSound(name);
        if (sound == null) return;
        
        
        sound.Source.Play();
    }
    
    public void Stop(string name)
    {
        Sound sound = GetSound(name);
        if (sound == null) return;
        
        sound.Source.Stop();
    }

    public void ChangeVolume(string name, float newVolumeValue)
    {
        Sound sound = GetSound(name);
        if (sound == null) return;

        sound.Volume = newVolumeValue;
    }

    public void ChangePitch(string name, float newPitchValue)
    {
        Sound sound = GetSound(name);
        if (sound == null) return;

        sound.Pitch = newPitchValue;
    }

    private Sound GetSound(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.Name == name);
        
        if (sound == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return null;
        }
        
        return sound;
    }
}

[System.Serializable]
public class Sound
{
    public string Name = "Audio identifier";

    public AudioClip AudioClip;

    [Range(0f, 1f)] public float Volume = 1;
    [Range(.1f, 3f)] public float Pitch = 1;

    public bool Loop;

    [HideInInspector]
    public AudioSource Source;
}