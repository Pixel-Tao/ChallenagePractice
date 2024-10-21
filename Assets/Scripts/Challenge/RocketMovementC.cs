using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class RocketMovementC : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    private readonly float SPEED = 10f;
    private readonly float ROTATIONSPEED = 0.02f;

    private float highScore = -1;

    public static Action<float> OnHighScoreChanged;
    
    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!(highScore < transform.position.y)) return;
        highScore = transform.position.y;
        OnHighScoreChanged?.Invoke(highScore);
    }

    public void ApplyMovement(float inputX)
    {
        Rotate(inputX);
    }

    public void ApplyBoost()
    {
        _rb2d.AddForce(transform.up * SPEED, ForceMode2D.Impulse);
    }

    private void Rotate(float inputX)
    {
        // 움직임이 없으면 회전을 하지 않음
        if (inputX == 0) return;

        // 움직임에 따라 회전을 바꿈 -> 회전을 바꾸고 그 방향으로 발사를 해야 그쪽으로 가겠죠?
        // 삼각함수를 이용하여 회전값을 계산
        float rotZ = -inputX * Mathf.Rad2Deg;
        // 회전값을 Quaternion으로 변환
        Quaternion rotation = Quaternion.Euler(0f, 0f, rotZ);
        // 회전값을 부드럽게 적용
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, ROTATIONSPEED);
    }
}