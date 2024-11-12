using System;
using UnityEngine;

[Serializable]
public class SkillGrenadeThrow : SkillCommandBase
{
    public override void Execute()
    {
        if (isCoolDown) return;
        
        Debug.Log("Grenade Throw!");
        GameObject go = PoolManager.Instance.Spawn(skillSO.projectilePrefab, owner.position);
        Projectile projectile = go.GetComponent<ParabolicProjectile>();
        projectile.Play(owner.transform, target);
        base.Execute();
    }
}