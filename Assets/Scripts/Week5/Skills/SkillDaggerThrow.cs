using System;
using UnityEngine;

[Serializable]
public class SkillDaggerThrow : SkillCommandBase
{
    public override void Execute()
    {
        if (isCoolDown) return;
        
        Debug.Log("Dagger Throw!");
        GameObject go = PoolManager.Instance.Spawn(skillSO.projectilePrefab, owner.position);
        Projectile projectile = go.GetComponent<LinearProjectile>();
        projectile.Play(owner.transform, target);
        base.Execute();
    }
}