using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // 플레이어가 공격할 시 카메라를 흔들어 타격감을 만듦.
       
    // 카메라 흔들림의 지속 시간.
    public float stopTime;

    // 흔들릴 카메라와 좌표.
    public Transform shakeCam;
    public Vector3 shake;

    public void CamShake()
    {        
        shakeCam.localPosition = shake;          
        // 코루틴 실행.
        StartCoroutine("ReturnTimeScale");             
    }

    // 코루틴 생성.
    IEnumerator ReturnTimeScale()
    {
        // stopTime만큼 시간이 지난 후에 카메라를 원래대로 함.
        yield return new WaitForSecondsRealtime(stopTime);        
        shakeCam.localPosition = Vector3.zero;        
    }

}
