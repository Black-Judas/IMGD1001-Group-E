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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("menuTheme");
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

    public void PlaySFX(string name)
    {
        Sound sound = Array.Find(music, i => i.name == name);

        if (sound == null)
        {
            Debug.Log("Sounds Not Found");
        }
        else
        {
            sfxSource.PlayOneShot(sound.clip);
        }
    }
}
