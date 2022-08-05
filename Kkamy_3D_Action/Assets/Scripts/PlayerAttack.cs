using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{ 
    // 공격 애니메이션을 실행하기 전에 플레이어 중심축을 카메라 중심축과 맞춤.

    // 콤보 시스템을 구현하기 위한 변수들.    
    Animator playerAnim;    // 플레이어의 애니메이터.

    public int comboStep;    // 콤보 진행 단계.
    bool comboPossible;      // 콤보 가능 여부 표시.    
    bool inputSmash;          // 스매쉬 키의 입력 여부.
    
    void Start()
    {
        playerAnim = gameObject.GetComponent<Animator>();
    }

    // 콤보의 시작점. 콤보를 가능하게 함.
    public void ComboPossible()
    {
        comboPossible = true;
    }

    // 입력 키와 콤보 단계에 따라 다음 동작을 재생하는 기능.
    public void NextAtk()
    {
        // 다음 공격이 스매쉬가 아니라면. (일반 공격이라면)
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
        // 다음 공격이 스매쉬라면.
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

    // 콤보 단계를 다시 처음으로 초기화시키는 기능.
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

        // 콤보스텝이 0이라면 첫번째 기본 공격을 사용.
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
                comboPossible = false;  // 막 눌렀을 때, 무차별 입력을 방지.
                comboStep++;
            }
        }
    }

    void SmashAttack()
    {
        if (comboPossible)
        {
            comboPossible = false;  // 막 눌렀을 때, 무차별 입력을 방지.
            inputSmash = true;       // 스매쉬 키가 눌렸다는 것을 알려줌.
        }
    }
    
    void Update()
    {
        // 왼쪽 마우스 클릭은 노멀 어택, 오른쪽 마우스 클릭은 스매쉬 어택.
        if (Input.GetMouseButtonDown(0)) NormalAttack();
        if(Input.GetMouseButtonDown(1)) SmashAttack();
    }
}
