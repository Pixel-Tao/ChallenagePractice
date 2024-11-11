using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplyProjectile : Projectile
{
    [Header("멀티샷 설정")]
    public float spreadAngle; // 방사 각도 -spreadAngle ~ spreadAngle 까지
    public int numberOfProjectiles; // 멀티샷 수

    protected override void Move()
    {
        
    }

    public override void Play(Transform startTransform, Transform targetTransform)
    {
        spriteRenderer.enabled = false;

        float angleStep = spreadAngle / (numberOfProjectiles - 1);
        float startAngle = -spreadAngle / 2;

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            GameObject go = new GameObject("MultiplyLinearProjectile");
            LinearProjectile projectile = go.AddComponent<LinearProjectile>();
            projectile.direction = direction;
            float angle = startAngle + (angleStep * i);
            projectile.direction = Quaternion.Euler(0, 0, angle) * direction;
            projectile.speed = speed;
            projectile.destroyOnTime = true;
            projectile.Play(startTransform);
        }
    }
}
