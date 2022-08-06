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

    [HideInInspector] public float mouseX;   // 마우스의 x축 이동값.
    [HideInInspector] public float mouseY;   // 마우스의 y축 이동값.
    float wheel;      // 마우스의 휠 이동값.      

    void Move()
    {
        Vector3 tmp = cam.position;
        mouseX += Input.GetAxis("Mouse X");
        // 마우스 움직임과 시점 이동 방향을 반대로 하기 위해 -1을 곱함.
        mouseY += Input.GetAxis("Mouse Y") * -1;

        // 카메라가 너무 많이 회전하는 것을 방지하는 코드.
        if (mouseY > 2) mouseY = 2;
        if (mouseY < -1) mouseY = -1;

        /** 카메라의 중심축을 마우스 이동값만큼 회전시킴.
         * 카메라는 중심축의 자식 오브젝트이므로 함께 회전함. */
        cam_CentralAxis.rotation = Quaternion.Euler(new Vector3(
            cam_CentralAxis.rotation.x + mouseY,
            cam_CentralAxis.rotation.y + mouseX, 0f) * camSpeed);        
    }

    void Zoom()
    {
        wheel += Input.GetAxis("Mouse ScrollWheel") * 10;

        if (wheel >= -4) wheel = -4;
        if (wheel <= -10) wheel = -10;

        // 카메라의 로컬포지션(카메라 축 기준)에서 z값을 바꿔줌.
        cam.localPosition = new Vector3(0, 5, wheel);
    }

    void Start()
    {
        wheel = -7f;
        mouseY = 1f;
    }
        
    void Update()
    {
        Move();
        Zoom();
    }
}
