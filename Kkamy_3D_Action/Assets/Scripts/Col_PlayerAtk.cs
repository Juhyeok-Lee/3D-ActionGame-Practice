using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Col_PlayerAtk : MonoBehaviour
{
    public Animator playerAnim;
    public CameraShake camShake;    

    // 플레이어가 적을 공격했을 때 타격음을 발생시킴.
    private AudioSource atkSound;

    public PlayerAttack playerAtk;
    public string type_Atk; // 공격 타입. 노멀/스매쉬.

    int comboStep;
    public string dmg; // 데미지를 문자열로 바꾸어 출력할 예정.
    public TextMeshProUGUI dmgText;

    private void Awake()
    {
        atkSound = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        // 활성화될 때, 콤보스텝을 가져옴.
        comboStep = playerAtk.comboStep;
    }

    // 플레이어의 공격 충돌체가 적의 히트박스와 트리거 충돌했을 때.
    private void OnTriggerEnter(Collider other)
    {
        // 본래 태그 비교를 ==로 했으나 CompareTag를 이용하도록 바꿈.
        if (other.CompareTag("HitBox_Enemy"))
        {
            // 충돌 발생 시 타격음을 재생함.
            atkSound.Play();

            // 어떤 공격 타입인지, 콤보스텝이 몇인지 표시.
            dmg = string.Format("{0} +{1}", type_Atk, comboStep);

            if (comboStep == 0)
            {
                dmg = "Counter!";
            }
            
            dmgText.text = dmg;
            dmgText.gameObject.SetActive(true);

            // 적을 공격했을 때 카메라 흔들림 적용.
            camShake.CamShake();

            // Invoke() 메소드를 이용해 타격이 발생했을 때, 플레이어 애니메이션을 느리게 하는 방법으로 역경직 구현.
            CancelInvoke();
            AnimDelay();
            Invoke("AnimReset", 0.05f);
        }
    }

    // 플레이어 애니메이터를 지연시킴.
    void AnimDelay()
    {
        playerAnim.speed = 0.1f;
    }

    // 플레이어 애니메이터 스피드를 원래대로 바꿈.
    void AnimReset()
    {
        playerAnim.speed = 1.0f;
    }
}
