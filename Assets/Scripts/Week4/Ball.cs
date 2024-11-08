using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private CircleCollider2D circleCollider;
    private MovePanel targetPanel;

    private float speed = 4f;
    private bool isOut = false;

    private void Awake()
    {
        spriteRenderer = gameObject.GetOrAddComponent<SpriteRenderer>();
        circleCollider = gameObject.GetOrAddComponent<CircleCollider2D>();
        rb = gameObject.GetOrAddComponent<Rigidbody2D>();
    }

    private void Start()
    {
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/Circle");
        rb.gravityScale = 0;
        //rb.sharedMaterial = Resources.Load<PhysicsMaterial2D>("Materials/BallPhysicsMaterial");
    }

    private void Update()
    {
        CheckDead();
    }

    public void CheckDead()
    {
        if (transform.position.x < GameManagerWeek4.Instance.minVector.x || transform.position.x > GameManagerWeek4.Instance.maxVector.x ||
            transform.position.y < GameManagerWeek4.Instance.minVector.y || transform.position.y > GameManagerWeek4.Instance.maxVector.y)
        {
            speed = 4f;
            transform.position = Vector3.zero;
            GameManagerWeek4.Instance.ResetScore();
            Shoot();
        }
    }

    public void SetStartTarget(MovePanel target)
    {
        this.targetPanel = target;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            speed += 0.05f;
            Shoot();
            GameManagerWeek4.Instance.IncreaseScore();
        }
    }

    private Vector2 RandomDirection()
    {
        targetPanel = GameManagerWeek4.Instance.GetRandomPanel(targetPanel);
        Vector3 targetVector = (targetPanel.transform.position - transform.position).normalized;
        float angle = Random.Range(-25, 25);
        targetVector = Quaternion.Euler(0, 0, angle) * targetVector;
        Debug.Log($"{targetPanel.name}, {targetPanel.MovementType} dest : {targetVector}");

        return targetVector.normalized;
    }

    public void Shoot()
    {
        Vector2 direction = RandomDirection();
        rb.velocity = Vector2.zero;
        rb.AddForce(direction * speed, ForceMode2D.Impulse);
    }
}
