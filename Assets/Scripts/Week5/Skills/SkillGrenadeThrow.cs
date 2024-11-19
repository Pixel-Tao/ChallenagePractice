using System;
using UnityEngine;

[Serializable]
public class SkillGrenadeThrow : SkillCommandBase
{
    public override void Execute()
    {
        if (isCoolDown) return;
        
        Debug.Log("Grenade Throw!");
        // 프로젝타일 풀링
        GameObject go = PoolManager.Instance.Spawn(skillSO.projectilePrefab, owner.position);
        // 풀링된 프로젝타일을 가져와서 Play 메소드 실행
        Projectile projectile = go.GetComponent<Projectile>();
        // Projectile projectile = go.GetComponent<ParabolicProjectile>();
        projectile.Play(owner.transform, target);
        base.Execute();
    }
}