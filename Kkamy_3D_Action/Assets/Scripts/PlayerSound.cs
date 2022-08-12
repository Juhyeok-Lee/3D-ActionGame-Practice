using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    AudioSource playAudio;          // ������ҽ�
    public AudioClip walkA;         // �ȱ� ȿ���� A
    public AudioClip walkB;         // �ȱ� ȿ���� B
    public AudioClip normalAtk;     // �Ϲݰ��� ȿ����
    public AudioClip smashAtk;      // ���Ž����� ȿ����
    public AudioClip hit;           // �ǰ� ȿ����
    public AudioClip parry;         // �и� ȿ����

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
