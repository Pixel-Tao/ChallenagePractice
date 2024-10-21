using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MyCircle : MonoBehaviour
{
    public void OnSpace()
    {
        AudioManager.Instance.PlayBgm();
    }
}
