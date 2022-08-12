using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Escape : MonoBehaviour
{ 
    public void OnClick()
    {
        GameManager.instance.GameQuit();
    }
}
