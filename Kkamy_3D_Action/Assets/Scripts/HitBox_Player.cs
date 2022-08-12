using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox_Player : MonoBehaviour
{
    // 플레이어의 애니메이터
    public Animator playerAni;

    public HitBox_Enemy enemyHitBox;
       
    private void OnTriggerEnter(Collider other)
    {
        // 적의 공격 콜라이더와 충돌했을 때.
        if (other.CompareTag("Col_EnemyAtk"))
        {
            // 히트박스의 태그가 HitBox라면 경직을 입음.
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
