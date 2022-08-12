using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitBox_Enemy : MonoBehaviour
{
    // 몬스터의 애니메이터
    public Animator golemAni;
    public Image hitMark;       // 타격 이펙트.
        
    [HideInInspector] public AudioSource enemyAudio; // 피격음을 출력하기 위한 오디오소스.
    public float rigidityPoint;    // 경직 게이지. 0 아래로 떨어지면 몬스터가 경직 상태가 됨.

    private void Awake()
    {
        enemyAudio = GetComponent<AudioSource>();
        rigidityPoint = 100f;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어의 공격 콜라이더와 충돌했을 때.
        if (other.CompareTag("Col_PlayerAtk"))
        {
            /** 나의 태그가 HitBox_Enemy이면. 
             * (HitBox에 추가할 것이므로 불필요한 코드지만 견고성을 위해 추가.) */
            if (gameObject.CompareTag("HitBox_Enemy"))
            {
                // 공격 당할 때마다 경직 게이지가 감소.
                rigidityPoint -= 17f;

                hitMark.transform.position = Camera.main.WorldToScreenPoint(other.transform.position);
                hitMark.gameObject.SetActive(true);

                if (rigidityPoint < 0f)
                {                
                    // 경직 게이지가 0 밑이 되면 100으로 초기화하고 경직 애니메이션을 재생.
                    rigidityPoint = 100f;
                    golemAni.Play("Golem_Rigidity");
                    enemyAudio.Play();
                }                
            }
        }
    }

    private void Update()
    {
        // 경직 게이지가 최대 100까지 서서히 회복됨.
        if (rigidityPoint <= 100f)
        {
            rigidityPoint += Time.deltaTime * 2f;
        }        
    }
}
