using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // �÷��̾ ������ �� ī�޶� ���� Ÿ�ݰ��� ����.
       
    // ī�޶� ��鸲�� ���� �ð�.
    public float stopTime;

    // ��鸱 ī�޶�� ��ǥ.
    public Transform shakeCam;
    public Vector3 shake;

    public void CamShake()
    {        
        shakeCam.localPosition = shake;          
        // �ڷ�ƾ ����.
        StartCoroutine("ReturnTimeScale");             
    }

    // �ڷ�ƾ ����.
    IEnumerator ReturnTimeScale()
    {
        // stopTime��ŭ �ð��� ���� �Ŀ� ī�޶� ������� ��.
        yield return new WaitForSecondsRealtime(stopTime);        
        shakeCam.localPosition = Vector3.zero;        
    }

}
