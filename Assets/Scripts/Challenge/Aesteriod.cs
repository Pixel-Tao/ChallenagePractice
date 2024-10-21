using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AesteriodSizeType
{
    Small,
    Medium,
    Large
}

public enum AesteriodMoveType
{
    Linear,
    Orbital
}

public class Aesteriod : MonoBehaviour
{
    [SerializeField] private AesteriodSizeType sizeType;
    [SerializeField] private AesteriodMoveType moveType;
    [SerializeField][Range(0, 10)] private float speed = 1f;
    [SerializeField][Range(0, 10)] private float duration = 5f;

    private float time = 0f;
    private Vector3 direction;
    [SerializeField] private Vector3 parentPos;
    private Vector3 centerPosition;

    public void SetInfo(AesteriodSizeType sizeType, AesteriodMoveType moveType, Vector2 dir)
    {
        direction = dir;
        this.sizeType = sizeType;
        this.moveType = moveType;
        time = 0;
        centerPosition = transform.position;
    }

    private void Start()
    {
        ChangeSize(sizeType);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= duration)
        {
            Destroy(gameObject);
            return;
        }

        if (moveType == AesteriodMoveType.Linear)
            LinearMove();
        else if (moveType == AesteriodMoveType.Orbital)
            OrbitalMove();
    }

    private void ChangeSize(AesteriodSizeType size)
    {
        switch (size)
        {
            case AesteriodSizeType.Small:
                transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                break;
            case AesteriodSizeType.Medium:
                transform.localScale = new Vector3(1f, 1f, 1f);
                break;
            case AesteriodSizeType.Large:
                transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                break;
        }
    }

    private float radius = 5f; // 공전 반지름
    private float angle = 0f; // 현재 각도
    private void OrbitalMove()
    {
        // 각도를 시간에 따라 증가시킴
        angle += speed * Time.deltaTime;

        // 공전하는 물체의 새로운 좌표를 계산
        float x = centerPosition.x + Mathf.Cos(angle) * radius;
        float y = centerPosition.y + Mathf.Sin(angle) * radius;

        // 물체의 위치를 업데이트
        transform.position = new Vector2(x, y);
    }

    private void LinearMove()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
