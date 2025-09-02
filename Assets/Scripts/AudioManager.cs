using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// This script manages background music and sound effects (SFX)
// It uses the Singleton pattern to make sure only one AudioManager exists across all scenes
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        // Ensure only one instance of AudioManager exists (Singleton pattern)
        if (instance == null)
        {
            instance = this;

            // Don't destroy this AudioManager when loading new scenes
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If another AudioManager exists, destroy it (avoid duplicates)
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Automatically start playing background music at game start
        PlayMusic("Game Music");
    }

    // Plays a music track by name
    public void PlayMusic(string name)
    {
        // Look for the sound in the musicSounds array by its name
        Sound s = Array.Find(musicSounds, x => x.name == name);

        // If the sound is not found, log a warning
        if (s != null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    // Plays a sound effect (SFX) by name
    public void PlaySFX(string name)
    {
        // Debug log all available SFX names (useful for checking what sounds are loaded)
        foreach (Sound sound in sfxSounds)
        {
            Debug.Log("Sound: " + sound.name);
        }

        Sound s = Array.Find(sfxSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    // Adjusts the background music volume (0.0f = mute, 1.0f = full volume)
    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    // Adjusts the SFX volume (0.0f = mute, 1.0f = full volume)
    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
