using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrSoundManager : MonoBehaviour
{
    public AudioClip music;
    //collect stock sounds
    public AudioClip[] creepySounds;
    public AudioSource soundPlayer, musicPlayer;
    public float nextSound;
    public Vector2 ranTime, ranVolume;


    private void Start()
    {
        //play background music
        musicPlayer.clip = music;
        musicPlayer.Play();

        //start random sounds
        if (creepySounds.Length > 0)
        { nextSound = Random.Range(ranTime.x, ranTime.y); }
    }
    private void Update()
    {
        if (creepySounds.Length > 0)
        {
            //count down to next sound
            if (nextSound <= 0)
            {
                PlayNewSound();
            }
            else
            {
                nextSound -= Time.deltaTime;
            }
        }
    }

    void PlayNewSound()
    {
        //pick a sound and random volume
        soundPlayer.clip = creepySounds[Random.Range(0, creepySounds.Length)];
        soundPlayer.volume = Random.Range(ranVolume.x, ranVolume.y);

        soundPlayer.Play();

        //set random delay until next sound
        nextSound = Random.Range(ranTime.x, ranTime.y);
    }


}
