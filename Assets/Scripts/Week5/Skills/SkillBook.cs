using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillBook : MonoBehaviour
{
    private event Action SkillCheckCooldownEvent;
    Dictionary<int, SkillCommandBase> skillCommands = new Dictionary<int, SkillCommandBase>();

    private void Update()
    {
        SkillCheckCooldownEvent?.Invoke();
    }

    public void AddSkill(SkillSO skillSO)
    {
        SkillCommandBase skillCommand = CreateSkillCommand(skillSO);
        if (skillCommand != null)
        {
            skillCommands.Add(skillSO.id, skillCommand);
            // 임시로 스킬을 퀵슬롯에 자동 추가.
            QuickSlotKey key = QuickSlotManagerWeek5.Instance.GetEmptyQuickSlotKey();
            QuickSlotManagerWeek5.Instance.SetQuickSlot(key, skillSO.id);
        }
    }
    private SkillCommandBase CreateSkillCommand(SkillSO skillSO)
    {
        string skillClassName = skillSO.skillClassName;
        Type type = Type.GetType(skillClassName);
        if (type == null) return null;
        SkillCommandBase skillCommand = Activator.CreateInstance(type) as SkillCommandBase;
        skillCommand.SetData(skillSO, transform);
        SkillCheckCooldownEvent += skillCommand.CheckCoolDown;
        return skillCommand;
    }

    public void InvokeExecute(int id, Transform target = null)
    {
        if (skillCommands.TryGetValue(id, out SkillCommandBase skillCommand))
        {
            skillCommand.SetTarget(target);
            skillCommand.Execute();
        }
    }
    
    public SkillCommandBase GetSkillById(int id)
    {
        return skillCommands.GetValueOrDefault(id);
    }
}