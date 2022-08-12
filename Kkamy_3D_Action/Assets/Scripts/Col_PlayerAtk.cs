using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Col_PlayerAtk : MonoBehaviour
{
    public Animator playerAnim;
    public CameraShake camShake;    

    // �÷��̾ ���� �������� �� Ÿ������ �߻���Ŵ.
    private AudioSource atkSound;

    public PlayerAttack playerAtk;
    public string type_Atk; // ���� Ÿ��. ���/���Ž�.

    int comboStep;
    public string dmg; // �������� ���ڿ��� �ٲپ� ����� ����.
    public TextMeshProUGUI dmgText;

    private void Awake()
    {
        atkSound = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        // Ȱ��ȭ�� ��, �޺������� ������.
        comboStep = playerAtk.comboStep;
    }

    // �÷��̾��� ���� �浹ü�� ���� ��Ʈ�ڽ��� Ʈ���� �浹���� ��.
    private void OnTriggerEnter(Collider other)
    {
        // ���� �±� �񱳸� ==�� ������ CompareTag�� �̿��ϵ��� �ٲ�.
        if (other.CompareTag("HitBox_Enemy"))
        {
            // �浹 �߻� �� Ÿ������ �����.
            atkSound.Play();

            // � ���� Ÿ������, �޺������� ������ ǥ��.
            dmg = string.Format("{0} +{1}", type_Atk, comboStep);

            if (comboStep == 0)
            {
                dmg = "Counter!";
            }
            
            dmgText.text = dmg;
            dmgText.gameObject.SetActive(true);

            // ���� �������� �� ī�޶� ��鸲 ����.
            camShake.CamShake();

            // Invoke() �޼ҵ带 �̿��� Ÿ���� �߻����� ��, �÷��̾� �ִϸ��̼��� ������ �ϴ� ������� ������ ����.
            CancelInvoke();
            AnimDelay();
            Invoke("AnimReset", 0.05f);
        }
    }

    // �÷��̾� �ִϸ����͸� ������Ŵ.
    void AnimDelay()
    {
        playerAnim.speed = 0.1f;
    }

    // �÷��̾� �ִϸ����� ���ǵ带 ������� �ٲ�.
    void AnimReset()
    {
        playerAnim.speed = 1.0f;
    }
}
