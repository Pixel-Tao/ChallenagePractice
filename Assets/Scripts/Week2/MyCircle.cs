using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MyCircle : MonoBehaviour
{
    public void Start()
    {
        AudioManager.Instance.PlayBgm();
    }

    public void OnSpace()
    {
        ShootShape();
    }

    public void ShootShape()
    {
        string name = Random.Range(0, 2) > 0 ? "Triangle" : "Square";

        GameObject go = PoolManager.Instance.Spawn(name, transform.position);
        go.GetComponent<Shape>()?.Init();

    }
}
