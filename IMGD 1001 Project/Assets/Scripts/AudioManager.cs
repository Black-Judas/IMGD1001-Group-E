using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] music, sfx;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(music, i => i.name == name);

        if (sound == null)
        {
            Debug.Log("Sounds Not Found");
        }
        else
        {
            musicSource.clip = sound.clip;
            musicSource.Play();
        }   
    }

    public void PlaySFX(string name, float volume = 1)
    {
        Sound sound = Array.Find(sfx, i => i.name == name);

        if (sound == null)
        {
            Debug.Log("Sounds Not Found");
        }
        else
        {
            Debug.Log("Playing " + sound.clip + " at " + volume + " volume");
            sfxSource.PlayOneShot(sound.clip, volume * sfxSource.volume);
        }
    }
}
