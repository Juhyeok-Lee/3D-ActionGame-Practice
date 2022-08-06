using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObj : MonoBehaviour
{
    // ������Ʈ�� ��Ȱ��ȭ�ϴ� �ð� ����.
    public float dTime;

    private void OnEnable()
    {
        /** Disable()�� dTime �ڿ� �����ϵ��� ��.
         * Ȱ��ȭ�ǰ� ���� �ð��� ������ �ڵ����� ��Ȱ��ȭ. */
        CancelInvoke();     // ��� �κ�ũ ȣ���� ������.
        // �κ�ũ �޼ҵ�� �� ������ �ð��� �޾� �ش� �ð��� ������ �� �޼ҵ带 �����ų �� �ִ�.
        // dTime�� ���� ��, Disable()�� �����.
        // ���� �ð� �ڿ� �Լ��� �����Ű�� ����� �ڷ�ƾ�� ������, �κ�ũ�� ������ ������.
        // ���� ĵ���κ�ũ�� ���� Disable�� �ߺ����� ����Ǵ� ���� ������.
        Invoke("Disable", dTime);
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }
}
