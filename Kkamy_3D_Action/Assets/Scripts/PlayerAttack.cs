using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    /** 공격 애니메이션을 실행하기 전에 플레이어 중심축을 카메라 중심축과 맞춤. 
     * 카메라 오브젝트는 x축 회전이 있지만, 플레이어 오브젝트는 x축 회전을 사용하지 않을 것이므로 y축 회전 값만을 대입해야 함.
     * 플레이어 중심축은 Move가 발생할 때에만 카메라 중심축과 맞춰지고 움직일 때 회전하는 것은 플레이어 오브젝트임. 
     * 따라서 플레이어의 로컬 회전이 아닌 월드 회전을 맞춰줘야 함.
     * 공격 방향이 즉각적으로 회전해야 하므로 보간을 사용하여 자연스럽게 회전하는 것이 아닌, 부자연스럽더라도 즉시 회전해야 함.
     * 따라서 공격 애니메이션이 실행되기 전에 플레이어 오브젝트의 forward에 카메라 축이 바라보는 y축 회전을 대입할 예정. */
    public Transform camAxis;       // 카메라 중심축의 forward를 가져오기 위해 변수를 선언.

    // 콤보 시스템을 구현하기 위한 변수들.    
    Animator playerAnim;    // 플레이어의 애니메이터.    
    bool comboPossible;     // 콤보 가능 여부 표시.    
    bool inputSmash;        // 스매쉬 키의 입력 여부.
    [HideInInspector] public int comboStep;    // 콤보 진행 단계.

    // Freeze와 UnFreeze를 사용하기 위해 enableAct를 받아오고자 선언함.
    public PlayerController playerController;
    // 패리 중일 때, 플레이어의 히트박스 태그를 변경하기 위해 선언함.
    public GameObject playerHitBox;

    bool isParry;       // 패리, 카운터 동작 중일 때는 공격을 할 수 없음.

    void Start()
    {
        playerAnim = gameObject.GetComponent<Animator>();
        isParry = false;
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
                // forward는 현재 오브젝트가 바라보는 방향의 방향벡터를 리턴한다. 따라서 y값을 제외하고 x,z값만을 전달함.
                transform.forward = new Vector3(camAxis.forward.x, 0, camAxis.forward.z);
                playerAnim.Play("Knight_NormalAtk_B");
            }
            if (comboStep == 3)
            {
                transform.forward = new Vector3(camAxis.forward.x, 0, camAxis.forward.z);
                playerAnim.Play("Knight_NormalAtk_C");
            }
        }
        // 다음 공격이 스매쉬라면.
        if (inputSmash)
        {
            if (comboStep == 1)
            {
                transform.forward = new Vector3(camAxis.forward.x, 0, camAxis.forward.z);
                playerAnim.Play("Knight_SmashAtk_A");
            }
            if (comboStep == 2)
            {
                transform.forward = new Vector3(camAxis.forward.x, 0, camAxis.forward.z);
                playerAnim.Play("Knight_SmashAtk_B");
            }
            if (comboStep == 3)
            {
                transform.forward = new Vector3(camAxis.forward.x, 0, camAxis.forward.z);
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
    }

    void NormalAttack()
    {
        // 콤보스텝이 0이라면 첫번째 기본 공격을 사용.
        if (comboStep == 0)
        {
            transform.forward = new Vector3(camAxis.forward.x, 0, camAxis.forward.z);
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
        if (!isParry)
        {
            // 왼쪽 마우스 클릭은 노멀 어택, 오른쪽 마우스 클릭은 스매쉬 어택.
            if (Input.GetMouseButtonDown(0)) NormalAttack();
            if (Input.GetMouseButtonDown(1)) SmashAttack();
        }

        // 다른 동작 중이 아닐 때, 왼쪽 컨트롤 키를 눌렀을 때 패리를 사용.
        if (playerController.enableAct)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                isParry = true;
                playerAnim.Play("Knight_Parry");
            }
        }
    }

    // 플레이어가 특정 동작을 실행 중에는 이동할 수 없도록 함.
    // 동작이 끝나면 UnFreeze하여 움직일 수 있음.
    void FreezePlayer()
    {
    playerController.enableAct = false;
    }

    void UnFreezePlayer()
    {
    playerController.enableAct = true;
    }

    void ResetParry()
    {
    isParry = false;
    }

    void ChangeTag(string t)
    {
    playerHitBox.tag = t;
    }
}

