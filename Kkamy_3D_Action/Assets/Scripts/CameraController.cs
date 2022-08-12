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

    [HideInInspector] public float rotateX;  
    [HideInInspector] public float rotateY;  
    float wheel;      // ���콺�� �� �̵���.      

    public DragOn dragOn;

    void Move()
    {
        Vector3 tmp = cam.position;
        rotateX += dragOn.xAngle;
        // ���콺 �����Ӱ� ���� �̵� ������ �ݴ�� �ϱ� ���� -1�� ����.
        rotateY += dragOn.yAngle * -1;
        dragOn.xAngle = 0;
        dragOn.yAngle = 0;

        // ī�޶� �ʹ� ���� ȸ���ϴ� ���� �����ϴ� �ڵ�.
        if (rotateY > 1f) rotateY = 1f;
        if (rotateY < -0.5f) rotateY = -0.5f;

        /** ī�޶��� �߽����� ���콺 �̵�����ŭ ȸ����Ŵ.
         * ī�޶�� �߽����� �ڽ� ������Ʈ�̹Ƿ� �Բ� ȸ����. */
        cam_CentralAxis.rotation = Quaternion.Euler(new Vector3(
            cam_CentralAxis.rotation.x + rotateY,
            cam_CentralAxis.rotation.y + rotateX, 0f) * camSpeed);        
    }

    void Zoom()
    {
        wheel += Input.GetAxis("Mouse ScrollWheel") * 10;

        if (wheel >= -6) wheel = -6;
        if (wheel <= -12) wheel = -12;

        // ī�޶��� ����������(ī�޶� �� ����)���� z���� �ٲ���.
        cam.localPosition = new Vector3(0, 5, wheel);
    }

    void Start()
    {
        // ī�޶�� �÷��̾� ������ �Ÿ�, ī�޶��� x�� ȸ���� ������ ������� �� �⺻������ ������.
        wheel = -10f;
        rotateY = 1f;
    }
        
    void Update()
    {
        Move();
        Zoom();
    }
}
