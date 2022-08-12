using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // 카메라를 움직이는 컴포넌트 제작.

    // 헤더를 이용하여 인스펙터 창에서 카메라 관련 변수들을 묶어 표시.
    [Header("Camera")]
    public Transform cam_CentralAxis;
    public Transform cam;
    public float camSpeed;  // 카메라 회전 속도.

    [HideInInspector] public float rotateX;  
    [HideInInspector] public float rotateY;  
    float wheel;      // 마우스의 휠 이동값.      

    public DragOn dragOn;

    void Move()
    {
        Vector3 tmp = cam.position;
        rotateX += dragOn.xAngle;
        // 마우스 움직임과 시점 이동 방향을 반대로 하기 위해 -1을 곱함.
        rotateY += dragOn.yAngle * -1;
        dragOn.xAngle = 0;
        dragOn.yAngle = 0;

        // 카메라가 너무 많이 회전하는 것을 방지하는 코드.
        if (rotateY > 1f) rotateY = 1f;
        if (rotateY < -0.5f) rotateY = -0.5f;

        /** 카메라의 중심축을 마우스 이동값만큼 회전시킴.
         * 카메라는 중심축의 자식 오브젝트이므로 함께 회전함. */
        cam_CentralAxis.rotation = Quaternion.Euler(new Vector3(
            cam_CentralAxis.rotation.x + rotateY,
            cam_CentralAxis.rotation.y + rotateX, 0f) * camSpeed);        
    }

    void Zoom()
    {
        wheel += Input.GetAxis("Mouse ScrollWheel") * 10;

        if (wheel >= -6) wheel = -6;
        if (wheel <= -12) wheel = -12;

        // 카메라의 로컬포지션(카메라 축 기준)에서 z값을 바꿔줌.
        cam.localPosition = new Vector3(0, 5, wheel);
    }

    void Start()
    {
        // 카메라와 플레이어 사이의 거리, 카메라의 x축 회전을 게임이 실행됐을 때 기본값으로 맞춰줌.
        wheel = -10f;
        rotateY = 1f;
    }
        
    void Update()
    {
        Move();
        Zoom();
    }
}
