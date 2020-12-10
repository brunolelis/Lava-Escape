using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource soundTheme;
    public AudioSource soundLava;
    public AudioSource soundDie;

    private void Awake()
    {
        instance = this;
    }

    public void PlayLava(AudioClip clip)
    {
        soundLava.clip = clip;
        soundLava.volume = Random.Range(.4f, .55f);
        soundLava.Play();
    }

    public void PlayDie(AudioClip clip)
    {
        soundDie.clip = clip;
        soundDie.volume = Random.Range(.5f, .7f);
        soundDie.Play();
    }

    public void PlayTheme(AudioClip clip)
    {
        soundTheme.clip = clip;
        soundTheme.Play();
    }
}
