using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem_AI : MonoBehaviour
{    
    Animator golemAni;          // 골렘의 애니메이터를 제어하기 위한 변수.
    public Transform target;    // 타겟(플레이어)의 트랜스폼.
    public float golemSpeed;   // 골렘의 이동속도.
    bool enableAct;                // 행동가능 상태 체크. (이동과 공격을 동시에 못하게.)
    int atkStep;                     // 공격단게.

    private void Start()
    {        
        golemAni = GetComponent<Animator>();
        enableAct = true;
    }

    // 골렘이 타겟을 바라보도록 하는 함수.
    void RotateGolem()
    {
        // 골렘에서 타겟을 향하는 방향벡터.
        Vector3 dir = target.position - transform.position;

        transform.localRotation =
            Quaternion.Slerp(transform.localRotation,
            Quaternion.LookRotation(dir), 5 * Time.deltaTime);
    }

    // 타겟과 자신이 10 이상 떨어져 있을 때만 이동.
    void MoveGolem()
    {
        if ((target.position - transform.position).magnitude >= 10)
        {
            golemAni.SetBool("Walk", true);
            transform.Translate(Vector3.forward * golemSpeed
                * Time.deltaTime, Space.Self);            
        }

        if ((target.position - transform.position).magnitude < 10)
        {
            golemAni.SetBool("Walk", false);
        }
    }

    void AtkGolem()
    {
        if ((target.position - transform.position).magnitude < 10)
        {
            // 공격 스텝에 따라 다른 공격을 행함.
            switch (atkStep)
            {
                case 0:
                    atkStep++;
                    golemAni.Play("Golem_AtkA");
                    break; 
                case 1:
                    atkStep++;
                    golemAni.Play("Golem_AtkB");
                    break;
                case 2:
                    atkStep = 0;
                    golemAni.Play("Golem_AtkC");
                    break;
            }
        }
    }

    // 골렘이 이동을 못하게 함.
    void FreezeGolem()
    {
        enableAct = false;
    }

    void UnFreezeGolem()
    {
        enableAct = true;
    }

    private void Update()
    {
        if (enableAct)
        {
            RotateGolem();
            MoveGolem();
        }
    }    
}
