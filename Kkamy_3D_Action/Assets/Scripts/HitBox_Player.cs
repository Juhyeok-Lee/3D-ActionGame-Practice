using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox_Player : MonoBehaviour
{
    // �÷��̾��� �ִϸ�����
    public Animator playerAni;

    public HitBox_Enemy enemyHitBox;
       
    private void OnTriggerEnter(Collider other)
    {
        // ���� ���� �ݶ��̴��� �浹���� ��.
        if (other.CompareTag("Col_EnemyAtk"))
        {
            // ��Ʈ�ڽ��� �±װ� HitBox��� ������ ����.
            if (gameObject.CompareTag("HitBox_Player"))
            {                
                playerAni.Play("Knight_Rigidity");                
            }
            else if (gameObject.CompareTag("Parry"))
            {
                playerAni.Play("Knight_Counter");
                enemyHitBox.rigidityPoint = 100f;                
                enemyHitBox.golemAni.Play("Golem_Rigidity");
                enemyHitBox.enemyAudio.Play();
            }
        }
    }    
}
