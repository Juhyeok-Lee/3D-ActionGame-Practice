using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem_AI : MonoBehaviour
{    
    Animator golemAni;          // ���� �ִϸ����͸� �����ϱ� ���� ����.
    public Transform target;    // Ÿ��(�÷��̾�)�� Ʈ������.
    public float golemSpeed;   // ���� �̵��ӵ�.
    bool enableAct;                // �ൿ���� ���� üũ. (�̵��� ������ ���ÿ� ���ϰ�.)
    int atkStep;                     // ���ݴܰ�.

    private void Start()
    {        
        golemAni = GetComponent<Animator>();
        enableAct = true;
    }

    // ���� Ÿ���� �ٶ󺸵��� �ϴ� �Լ�.
    void RotateGolem()
    {
        // �񷽿��� Ÿ���� ���ϴ� ���⺤��.
        Vector3 dir = target.position - transform.position;

        transform.localRotation =
            Quaternion.Slerp(transform.localRotation,
            Quaternion.LookRotation(dir), 5 * Time.deltaTime);
    }

    // Ÿ�ٰ� �ڽ��� 10 �̻� ������ ���� ���� �̵�.
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
            // ���� ���ܿ� ���� �ٸ� ������ ����.
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

    // ���� �̵��� ���ϰ� ��.
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
