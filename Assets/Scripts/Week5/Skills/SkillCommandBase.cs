using System;
using UnityEngine;

[Serializable]
public abstract class SkillCommandBase
{
    public Transform owner;
    public SkillSO skillSO;
    protected Transform target;
    protected bool isCoolDown;
    protected float lastUsedTime;

    public void CheckCoolDown()
    {
        if (isCoolDown && Time.time - lastUsedTime > skillSO.cooldown)
        {
            isCoolDown = false;
            Debug.Log($"{skillSO.displayName} Skill is ready!");
        }
    }
    
    public void SetData(SkillSO skillSO, Transform owner)
    {
        this.owner = owner;
        this.skillSO = skillSO;
    }
    public virtual void SetTarget(Transform target)
    {
        this.target = target;
    }
    public virtual void Execute()
    {
        isCoolDown = true;
        lastUsedTime = Time.time;
    }
}