using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitBox_Enemy : MonoBehaviour
{
    // ������ �ִϸ�����
    public Animator golemAni;
    public Image hitMark;       // Ÿ�� ����Ʈ.
        
    [HideInInspector] public AudioSource enemyAudio; // �ǰ����� ����ϱ� ���� ������ҽ�.
    public float rigidityPoint;    // ���� ������. 0 �Ʒ��� �������� ���Ͱ� ���� ���°� ��.

    private void Awake()
    {
        enemyAudio = GetComponent<AudioSource>();
        rigidityPoint = 100f;
    }

    private void OnTriggerEnter(Collider other)
    {
        // �÷��̾��� ���� �ݶ��̴��� �浹���� ��.
        if (other.CompareTag("Col_PlayerAtk"))
        {
            /** ���� �±װ� HitBox_Enemy�̸�. 
             * (HitBox�� �߰��� ���̹Ƿ� ���ʿ��� �ڵ����� �߰��� ���� �߰�.) */
            if (gameObject.CompareTag("HitBox_Enemy"))
            {
                // ���� ���� ������ ���� �������� ����.
                rigidityPoint -= 17f;

                hitMark.transform.position = Camera.main.WorldToScreenPoint(other.transform.position);
                hitMark.gameObject.SetActive(true);

                if (rigidityPoint < 0f)
                {                
                    // ���� �������� 0 ���� �Ǹ� 100���� �ʱ�ȭ�ϰ� ���� �ִϸ��̼��� ���.
                    rigidityPoint = 100f;
                    golemAni.Play("Golem_Rigidity");
                    enemyAudio.Play();
                }                
            }
        }
    }

    private void Update()
    {
        // ���� �������� �ִ� 100���� ������ ȸ����.
        if (rigidityPoint <= 100f)
        {
            rigidityPoint += Time.deltaTime * 2f;
        }        
    }
}
