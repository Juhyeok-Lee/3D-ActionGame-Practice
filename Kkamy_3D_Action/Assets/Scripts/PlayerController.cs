using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{    
    // �÷��̾� ������Ʈ�� �����̴� ������Ʈ ����.

    /** �÷��̾ ������ �� ������Ʈ ȸ���� ī�޶� ȸ���� ���߱� ����
     * ķ�� ȸ������ �������� ���� ������Ʈ ����. */
    public CameraController camController;

    [Header("Player")]
    public Transform playerAxis;
    public Transform player;
    public float playerSpeed;

    [HideInInspector] public Vector3 movement;    // �÷��̾��� �̵� ����.
    [HideInInspector] public bool enableAct;      // �÷��̾��� �̵� ���� ���θ� ǥ��.

    private void Awake()
    {
        enableAct = true;
    }

    void PlayerMove()
    {
        /** Ű���� �Է��� �����Ͽ� �÷��̾��� �̵� ������ ����.
         * x, z���� -1�� 1 ������ ������ ������. */
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, 
            Input.GetAxis("Vertical"));

        // �����Ʈ�� �����Ͱ� �ƴ� �� ĳ���� �̵�. (Ű���� �Է��� ���� ��.)
        if (movement != Vector3.zero)
        {
            // �÷��̾� ���� ȸ���� ī�޶� ȸ���� ������. x��, z�� ȸ���� ����.
            playerAxis.rotation = Quaternion.Euler(new Vector3(
                0, camController.cam_CentralAxis.rotation.y + 
                camController.mouseX, 0) * camController.camSpeed);

            // Translate �Լ��� �̿��� movement �������� �÷��̾� ���� ������.
            playerAxis.Translate(movement * Time.deltaTime * playerSpeed);

            /* Slerp �޼ҵ�� �÷��̾��� ���� ȸ�����κ���,
             * ��ǥ ����(movement�� ����Ű�� ����) ������ ȸ���� ��ȯ�Ѵ�.
             * RotateTowards�� �޸� ������ ����Ͽ� �ڿ������� ȸ���� �����Ѵ�.
             * movement�� ����3�̹Ƿ� ���ʹϾ����� ��ȯ�Ͽ���.*/
            player.localRotation = Quaternion.Slerp(player.localRotation, 
                Quaternion.LookRotation(movement), 5 * Time.deltaTime);

            /** movement�� 0�� �ƴ϶�� ���� �÷��̾ �����δٴ� ��.
             * ���� �÷��̾��� �ִϸ��̼� ���¸� ��ȯ��. */
            player.GetComponent<Animator>().SetBool("isMove", true);
        }
        else if (movement == Vector3.zero)
        {
            player.GetComponent<Animator>().SetBool("isMove", false);
        }

        // ī�޶� �߽� ���� �÷��̾� �������� ����ٴϵ��� ��.
        camController.cam_CentralAxis.position = player.position;
    }
    
    void Update()
    {
        // ����, �и�, ���� ������ �� �̵��� ���ϰ� ��.
        if (enableAct)
        {
            PlayerMove();
        }         
    }
}
