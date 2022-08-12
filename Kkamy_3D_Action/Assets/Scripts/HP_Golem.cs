using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_Golem : MonoBehaviour
{
    public float hpFull;
    public float hpCurrent;

    public Image hpBar_Front;
    public GameObject hpBar;

    public Animator golemAni;
    AudioSource golemHit;

    private void Start()
    {
        hpFull = 8000f;
        hpCurrent = hpFull;
        golemHit = GetComponent<AudioSource>();
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
            golemAni.Play("Golem_Rigidity");
            golemHit.Play();
            StartCoroutine("Die");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Col_PlayerAtk"))
        {
            hpCurrent -= Random.Range(100, 500);
        }
    }
    IEnumerator Die()
    {        
        yield return new WaitForSecondsRealtime(0.5f);
        hpBar.SetActive(false);
        golemAni.gameObject.SetActive(false);      
    }    
}
