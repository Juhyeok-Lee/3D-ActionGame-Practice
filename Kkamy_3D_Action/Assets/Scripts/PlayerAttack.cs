using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    /** ���� �ִϸ��̼��� �����ϱ� ���� �÷��̾� �߽����� ī�޶� �߽���� ����. 
     * ī�޶� ������Ʈ�� x�� ȸ���� ������, �÷��̾� ������Ʈ�� x�� ȸ���� ������� ���� ���̹Ƿ� y�� ȸ�� ������ �����ؾ� ��.
     * �÷��̾� �߽����� Move�� �߻��� ������ ī�޶� �߽���� �������� ������ �� ȸ���ϴ� ���� �÷��̾� ������Ʈ��. 
     * ���� �÷��̾��� ���� ȸ���� �ƴ� ���� ȸ���� ������� ��.
     * ���� ������ �ﰢ������ ȸ���ؾ� �ϹǷ� ������ ����Ͽ� �ڿ������� ȸ���ϴ� ���� �ƴ�, ���ڿ��������� ��� ȸ���ؾ� ��.
     * ���� ���� �ִϸ��̼��� ����Ǳ� ���� �÷��̾� ������Ʈ�� forward�� ī�޶� ���� �ٶ󺸴� y�� ȸ���� ������ ����. */
    public Transform camAxis;       // ī�޶� �߽����� forward�� �������� ���� ������ ����.

    // �޺� �ý����� �����ϱ� ���� ������.    
    Animator playerAnim;    // �÷��̾��� �ִϸ�����.    
    bool comboPossible;     // �޺� ���� ���� ǥ��.    
    bool inputSmash;        // ���Ž� Ű�� �Է� ����.
    [HideInInspector] public int comboStep;    // �޺� ���� �ܰ�.

    // Freeze�� UnFreeze�� ����ϱ� ���� enableAct�� �޾ƿ����� ������.
    public PlayerController playerController;
    // �и� ���� ��, �÷��̾��� ��Ʈ�ڽ� �±׸� �����ϱ� ���� ������.
    public GameObject playerHitBox;

    bool isParry;       // �и�, ī���� ���� ���� ���� ������ �� �� ����.

    void Start()
    {
        playerAnim = gameObject.GetComponent<Animator>();
        isParry = false;
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
                // forward�� ���� ������Ʈ�� �ٶ󺸴� ������ ���⺤�͸� �����Ѵ�. ���� y���� �����ϰ� x,z������ ������.
                transform.forward = new Vector3(camAxis.forward.x, 0, camAxis.forward.z);
                playerAnim.Play("Knight_NormalAtk_B");
            }
            if (comboStep == 3)
            {
                transform.forward = new Vector3(camAxis.forward.x, 0, camAxis.forward.z);
                playerAnim.Play("Knight_NormalAtk_C");
            }
        }
        // ���� ������ ���Ž����.
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

    // �޺� �ܰ踦 �ٽ� ó������ �ʱ�ȭ��Ű�� ���.
    public void ResetCombo()
    {
        comboPossible = false;
        inputSmash = false;
        comboStep = 0;
    }

    void NormalAttack()
    {
        // �޺������� 0�̶�� ù��° �⺻ ������ ���.
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
        if (!isParry)
        {
            // ���� ���콺 Ŭ���� ��� ����, ������ ���콺 Ŭ���� ���Ž� ����.
            if (Input.GetMouseButtonDown(0)) NormalAttack();
            if (Input.GetMouseButtonDown(1)) SmashAttack();
        }

        // �ٸ� ���� ���� �ƴ� ��, ���� ��Ʈ�� Ű�� ������ �� �и��� ���.
        if (playerController.enableAct)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                isParry = true;
                playerAnim.Play("Knight_Parry");
            }
        }
    }

    // �÷��̾ Ư�� ������ ���� �߿��� �̵��� �� ������ ��.
    // ������ ������ UnFreeze�Ͽ� ������ �� ����.
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

