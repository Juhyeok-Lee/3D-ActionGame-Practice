using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    AudioSource playAudio;
    public AudioClip walkA;
    public AudioClip walkB;
    public AudioClip normalAtk;
    public AudioClip smashAtk;

    void Start()
    {
        playAudio = GetComponent<AudioSource>();
    }

    void WalkA()
    {
        playAudio.clip = walkA;
        playAudio.Play();
    }

    void WalkB()
    {
        playAudio.clip = walkB;
        playAudio.Play();
    }

    void NormalAtk()
    {
        playAudio.clip = normalAtk;
        playAudio.Play();
    }

    void SmashAtk()
    {
        playAudio.clip = smashAtk;
        playAudio.Play();
    }

}
