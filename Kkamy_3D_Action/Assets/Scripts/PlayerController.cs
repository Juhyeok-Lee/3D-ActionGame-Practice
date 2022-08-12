using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{    
    // 플레이어 오브젝트를 움직이는 컴포넌트 제작.

    /** 플레이어가 움직일 때 오브젝트 회전을 카메라 회전과 맞추기 위해
     * 캠의 회전축을 가져오기 위해 컴포넌트 선언. */
    public CameraController camController;

    [Header("Player")]
    public Transform playerAxis;
    public Transform player;
    public float playerSpeed;

    [HideInInspector] public Vector3 movement;    // 플레이어의 이동 방향.
    [HideInInspector] public bool enableAct;      // 플레이어의 이동 가능 여부를 표시.

    private void Awake()
    {
        enableAct = true;
    }

    void PlayerMove()
    {
        /** 키보드 입력을 감지하여 플레이어의 이동 방향을 결정.
         * x, z값은 -1과 1 사이의 값으로 결정됨. */
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, 
            Input.GetAxis("Vertical"));

        // 무브먼트가 영벡터가 아닐 때 캐릭터 이동. (키보드 입력이 있을 때.)
        if (movement != Vector3.zero)
        {
            // 플레이어 축의 회전을 카메라 회전과 맞춰줌. x축, z축 회전은 없음.
            playerAxis.rotation = Quaternion.Euler(new Vector3(
                0, camController.cam_CentralAxis.rotation.y + 
                camController.mouseX, 0) * camController.camSpeed);

            // Translate 함수를 이용해 movement 방향으로 플레이어 축을 움직임.
            playerAxis.Translate(movement * Time.deltaTime * playerSpeed);

            /* Slerp 메소드는 플레이어의 현재 회전으로부터,
             * 목표 방향(movement가 가리키는 방향) 사이의 회전을 반환한다.
             * RotateTowards와 달리 보간을 사용하여 자연스러운 회전을 구현한다.
             * movement는 벡터3이므로 쿼터니언으로 변환하였다.*/
            player.localRotation = Quaternion.Slerp(player.localRotation, 
                Quaternion.LookRotation(movement), 5 * Time.deltaTime);

            /** movement가 0이 아니라는 것은 플레이어가 움직인다는 뜻.
             * 따라서 플레이어의 애니메이션 상태를 전환함. */
            player.GetComponent<Animator>().SetBool("isMove", true);
        }
        else if (movement == Vector3.zero)
        {
            player.GetComponent<Animator>().SetBool("isMove", false);
        }

        // 카메라 중심 축이 플레이어 포지션을 따라다니도록 함.
        camController.cam_CentralAxis.position = player.position;
    }
    
    void Update()
    {
        // 공격, 패리, 경직 상태일 땐 이동을 못하게 함.
        if (enableAct)
        {
            PlayerMove();
        }         
    }
}
