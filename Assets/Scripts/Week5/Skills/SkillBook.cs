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
        // 스킬 데이터를 받아와서 스킬커맨드를 생성
        SkillCommandBase skillCommand = CreateSkillCommand(skillSO);
        if (skillCommand != null)
        {
            skillCommands.Add(skillSO.id, skillCommand);
            // 임시로 비어있는 슬롯은 찾아서 스킬을 퀵슬롯에 자동 추가.
            // UI에서 스킬을 슬롯에 등록했다고 상상만 해봅시다.
            QuickSlotKey key = QuickSlotManagerWeek5.Instance.GetEmptyQuickSlotKey();
            QuickSlotManagerWeek5.Instance.SetQuickSlot(key, skillSO.id);
        }
    }
    private SkillCommandBase CreateSkillCommand(SkillSO skillSO)
    {
        // 스킬데이터를 특정 객체에 넣어줄 수 있도록 Class 이름을 string 값으로 가져옴
        string skillClassName = skillSO.skillClassName;
        Type type = Type.GetType(skillClassName);
        if (type == null) return null;
        SkillCommandBase skillCommand = Activator.CreateInstance(type) as SkillCommandBase;
        
        // 한가지 생각 볼것 skillClassName 가 없다면
        // if (skillSO.displayName == "멀티샷")
        //     skillCommand = new SkillMultishot();
        // else if (skillSO.displayName == "부메랑")
        //     skillCommand = new SkillBoomerang();
        // ... 이런식으로 하나하나 추가해야함
        
        // 스킬 데이터를 저장
        skillCommand.SetData(skillSO, transform);
        // 쿨타임을 실시간으로 체크하기 위해 이벤트 등록
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
        // skillCommands.TryGetValue(id, out SkillCommandBase skillCommand) 
        // return skillCommand;
        // 위 코드와 동일한 의미
        return skillCommands.GetValueOrDefault(id);
    }
}