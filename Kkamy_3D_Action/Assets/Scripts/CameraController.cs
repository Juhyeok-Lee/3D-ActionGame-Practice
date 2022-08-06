using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // ī�޶� �����̴� ������Ʈ ����.

    // ����� �̿��Ͽ� �ν����� â���� ī�޶� ���� �������� ���� ǥ��.
    [Header("Camera")]
    public Transform cam_CentralAxis;
    public Transform cam;
    public float camSpeed;  // ī�޶� ȸ�� �ӵ�.

    [HideInInspector] public float mouseX;   // ���콺�� x�� �̵���.
    [HideInInspector] public float mouseY;   // ���콺�� y�� �̵���.
    float wheel;      // ���콺�� �� �̵���.      

    void Move()
    {
        Vector3 tmp = cam.position;
        mouseX += Input.GetAxis("Mouse X");
        // ���콺 �����Ӱ� ���� �̵� ������ �ݴ�� �ϱ� ���� -1�� ����.
        mouseY += Input.GetAxis("Mouse Y") * -1;

        // ī�޶� �ʹ� ���� ȸ���ϴ� ���� �����ϴ� �ڵ�.
        if (mouseY > 2) mouseY = 2;
        if (mouseY < -1) mouseY = -1;

        /** ī�޶��� �߽����� ���콺 �̵�����ŭ ȸ����Ŵ.
         * ī�޶�� �߽����� �ڽ� ������Ʈ�̹Ƿ� �Բ� ȸ����. */
        cam_CentralAxis.rotation = Quaternion.Euler(new Vector3(
            cam_CentralAxis.rotation.x + mouseY,
            cam_CentralAxis.rotation.y + mouseX, 0f) * camSpeed);        
    }

    void Zoom()
    {
        wheel += Input.GetAxis("Mouse ScrollWheel") * 10;

        if (wheel >= -4) wheel = -4;
        if (wheel <= -10) wheel = -10;

        // ī�޶��� ����������(ī�޶� �� ����)���� z���� �ٲ���.
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
