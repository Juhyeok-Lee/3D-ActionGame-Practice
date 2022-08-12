using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_Player : MonoBehaviour
{
    public float hpFull;
    public float hpCurrent;

    public Image hpBar_Front;   
    public Animator playerAni;
    public PlayerSound playerHit;
    public GameObject player;
    public PlayerController playerController;

    private void Start()
    {
        hpFull = 1000f;
        hpCurrent = hpFull;
    }

    void SyncBar()
    {
        hpBar_Front.fillAmount = hpCurrent / hpFull;
    }

    private void Update()
    {
        SyncBar();
        if (hpCurrent < 0)
        {
            hpCurrent = 0f;
            playerAni.Play("Knight_Rigidity");
            playerHit.Hit();
            StartCoroutine("Die");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Col_EnemyAtk"))
        {
            if (gameObject.CompareTag("HitBox_Player"))
            {
                hpCurrent -= Random.Range(80, 120);
            }            
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSecondsRealtime(0.5f);        
        playerController.enableAct = false;
        player.SetActive(false);
    }
}
