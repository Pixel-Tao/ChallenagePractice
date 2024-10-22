using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private float rotateSpeed = 10f;
    private Vector3 direction = Vector3.zero;
    private float time = 0;
    private bool isLeftRotate = false;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        Timer();
        Move();
    }

    private void Move()
    {
        transform.position += direction * speed * Time.deltaTime;
        Vector3 rot = isLeftRotate ? Vector3.forward : -Vector3.forward;
        transform.Rotate(rot * rotateSpeed * Time.deltaTime, 1f);
    }

    private void Timer()
    {
        time += Time.deltaTime;
        if (time >= lifeTime)
        {
            PoolManager.Instance.Despawn(gameObject);
        }
    }

    public void Init()
    {
        time = 0;
        float randomAngle = Random.Range(0, 360);
        isLeftRotate = Random.Range(0, 2) > 0;
        direction = new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle), 0);
        direction = direction.normalized;
        spriteRenderer.color = new Color(Random.Range(0f, 2f), Random.Range(0f, 2f), Random.Range(0f, 2f));
    }
}
