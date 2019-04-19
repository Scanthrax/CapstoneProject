using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    public AudioSource correctSource;



    public AudioClip[] songs;
    public AudioSource musicSource;

    public AudioClip game1;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        PlayMusicAtStart();
    }


    public void PlayCorrectSound()
    {
        print("playing sound");
        correctSource.Play();
    }



    public void PlayMusicAtStart()
    {
        musicSource.clip = songs[Random.Range(0, songs.Length)];
        musicSource.Play();
    }

}
