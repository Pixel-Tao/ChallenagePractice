using System;
using UnityEngine;

[Serializable]
public class SkillBoomerang : SkillCommandBase
{
    public override void Execute()
    {
        if (isCoolDown) return;
        
        Debug.Log("Boomerang Throw!");
        GameObject go = PoolManager.Instance.Spawn(skillSO.projectilePrefab, owner.position);
        Projectile projectile = go.GetComponent<Projectile>();
        projectile.Play(owner.transform, target);
        base.Execute();
    }
}