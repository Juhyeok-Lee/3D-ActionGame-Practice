using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{ 
    // ���� �ִϸ��̼��� �����ϱ� ���� �÷��̾� �߽����� ī�޶� �߽���� ����.

    // �޺� �ý����� �����ϱ� ���� ������.    
    Animator playerAnim;    // �÷��̾��� �ִϸ�����.

    public int comboStep;    // �޺� ���� �ܰ�.
    bool comboPossible;      // �޺� ���� ���� ǥ��.    
    bool inputSmash;          // ���Ž� Ű�� �Է� ����.
    
    void Start()
    {
        playerAnim = gameObject.GetComponent<Animator>();
    }

    // �޺��� ������. �޺��� �����ϰ� ��.
    public void ComboPossible()
    {
        comboPossible = true;
    }

    // �Է� Ű�� �޺� �ܰ迡 ���� ���� ������ ����ϴ� ���.
    public void NextAtk()
    {
        // ���� ������ ���Ž��� �ƴ϶��. (�Ϲ� �����̶��)
        if (!inputSmash)
        {
            if (comboStep == 2)
            {                
                playerAnim.Play("Knight_NormalAtk_B");
            }
            if (comboStep == 3)
            {                
                playerAnim.Play("Knight_NormalAtk_C");
            }
        }
        // ���� ������ ���Ž����.
        if (inputSmash)
        {
            if (comboStep == 1)
            {                
                playerAnim.Play("Knight_SmashAtk_A");
            }
            if (comboStep == 2)
            {               
                playerAnim.Play("Knight_SmashAtk_B");
            }
            if (comboStep == 3)
            {                
                playerAnim.Play("Knight_SmashAtk_C");
            }
        }
    }

    // �޺� �ܰ踦 �ٽ� ó������ �ʱ�ȭ��Ű�� ���.
    public void ResetCombo()
    {
        comboPossible = false;
        inputSmash = false;
        comboStep = 0;

        playerAnim.SetBool("isAttack", false);
    }

    void NormalAttack()
    {
        playerAnim.SetBool("isAttack", true);        

        // �޺������� 0�̶�� ù��° �⺻ ������ ���.
        if (comboStep == 0)
        {            
            playerAnim.Play("Knight_NormalAtk_A");
            comboStep++;
            return;
        }
        if (comboStep != 0)
        {
            if (comboPossible)
            {
                comboPossible = false;  // �� ������ ��, ������ �Է��� ����.
                comboStep++;
            }
        }
    }

    void SmashAttack()
    {
        if (comboPossible)
        {
            comboPossible = false;  // �� ������ ��, ������ �Է��� ����.
            inputSmash = true;       // ���Ž� Ű�� ���ȴٴ� ���� �˷���.
        }
    }
    
    void Update()
    {
        // ���� ���콺 Ŭ���� ��� ����, ������ ���콺 Ŭ���� ���Ž� ����.
        if (Input.GetMouseButtonDown(0)) NormalAttack();
        if(Input.GetMouseButtonDown(1)) SmashAttack();
    }
}
