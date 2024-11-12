using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplyProjectile : Projectile
{
    public GameObject subProjectilePrefab;
    
    [Header("방사 범위")]
    public float spreadAngle;
    public int numberOfProjectiles;

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
            GameObject go = PoolManager.Instance.Spawn(subProjectilePrefab, transform.position);
            go.name = "MultiplyLinearProjectile";
            Projectile projectile = go.GetComponent<LinearProjectile>();
            projectile.direction = direction;
            float angle = startAngle + (angleStep * i);
            projectile.direction = Quaternion.Euler(0, 0, angle) * direction;
            projectile.speed = speed;
            projectile.shouldDestroyOnTime = true;
            projectile.Play(startTransform);
        }
    }
}
