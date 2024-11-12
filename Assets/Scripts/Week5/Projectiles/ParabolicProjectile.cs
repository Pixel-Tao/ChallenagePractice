using UnityEngine;

public class ParabolicProjectile : Projectile
{
    [Header("설정 값")]
    [Range(0f, 360f)] public float shootAngle = 45;
    [Range(0f, 20f)] public float force = 7;
    [Header("발사 속도")]
    [SerializeField] private Vector3 initVelocity;

    private float elapaedTime = 0;
    private Vector3 gravity = new Vector3(0, -9.81f, 0);

    protected override void Move()
    {
        elapaedTime += speed * Time.deltaTime;
        Vector3 position = startPosition + initVelocity * elapaedTime + 0.5f * gravity * Mathf.Pow(elapaedTime, 2);
        transform.position = position;
    }

    public override void Play(Transform startTransform, Transform targetTransform)
    {
        if (IsPlaying) return;

        IsPlaying = true;
        this.startPosition = startTransform.position;
        this.targetTransform = targetTransform;
        transform.position = this.startPosition;
        direction = (targetTransform.position - startPosition).normalized;

        float angleRad = shootAngle * Mathf.Deg2Rad;
        float fowardVelocity = Mathf.Cos(angleRad) * force;
        float upVelocity = Mathf.Sin(angleRad) * force;

        initVelocity = direction * fowardVelocity + Vector3.up * upVelocity;

        elapaedTime = 0f;
    }
}
