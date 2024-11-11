using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearProjectile : Projectile
{

    protected override void Move()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
