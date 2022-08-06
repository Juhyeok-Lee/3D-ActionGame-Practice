using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObj : MonoBehaviour
{
    // 오브젝트를 비활성화하는 시간 간격.
    public float dTime;

    private void OnEnable()
    {
        /** Disable()을 dTime 뒤에 실행하도록 함.
         * 활성화되고 일정 시간이 지나면 자동으로 비활성화. */
        CancelInvoke();     // 모든 인보크 호출을 종료함.
        // 인보크 메소드는 초 단위로 시간을 받아 해당 시간이 지연된 후 메소드를 실행시킬 수 있다.
        // dTime이 지난 후, Disable()이 실행됨.
        // 지연 시간 뒤에 함수를 실행시키는 방법은 코루틴도 있지만, 인보크가 구현이 간단함.
        // 위의 캔슬인보크를 통해 Disable이 중복으로 실행되는 것을 예방함.
        Invoke("Disable", dTime);
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }
}
