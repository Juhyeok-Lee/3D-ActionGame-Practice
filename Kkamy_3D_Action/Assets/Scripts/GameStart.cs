using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameStart : MonoBehaviour
{
    public void OnClick()
    {
        GameManager.instance.GameStart(1);
    }
}
