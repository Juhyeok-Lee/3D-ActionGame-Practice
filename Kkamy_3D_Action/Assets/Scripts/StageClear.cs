using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClear : MonoBehaviour
{
    public GameObject active;
    private void OnEnable()
    {
        StartCoroutine("Active");
    }

    IEnumerator Active()
    {
        yield return new WaitForSecondsRealtime(2f);
        active.SetActive(true);
    }    
}
