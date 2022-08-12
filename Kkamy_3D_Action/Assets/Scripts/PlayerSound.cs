using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    AudioSource playAudio;          // 오디오소스
    public AudioClip walkA;         // 걷기 효과음 A
    public AudioClip walkB;         // 걷기 효과음 B
    public AudioClip normalAtk;     // 일반공격 효과음
    public AudioClip smashAtk;      // 스매쉬공격 효과음
    public AudioClip hit;           // 피격 효과음
    public AudioClip parry;         // 패리 효과음

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
    public void Hit()
    {
        playAudio.clip = hit;
        playAudio.Play();
    }
    void Parry()
    {
        playAudio.clip = parry;
        playAudio.Play();
    }
}
