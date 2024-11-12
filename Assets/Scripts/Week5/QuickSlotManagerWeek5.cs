using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum QuickSlotKey
{
    None = 0,
    One = 1,
    Two = 2,
    Three = 3,
    Four = 4
}

public class QuickSlotManagerWeek5 : Singleton<QuickSlotManagerWeek5>
{
    public event Action<int> QuickSlotButtonPressed;
    public event Action<QuickSlotKey, int> QuickSlotAssigned;

    private Dictionary<QuickSlotKey, int> quickSlotMap = new Dictionary<QuickSlotKey, int>();

    public TextMeshProUGUI quickSlotText;
    private void Start()
    {
        foreach (QuickSlotKey key in Enum.GetValues(typeof(QuickSlotKey)))
        {
            if (key == QuickSlotKey.None) continue;
            quickSlotMap.Add(key, 0);
        }
        
        Refresh();
    }

    public void SetQuickSlot(QuickSlotKey key, int skillId)
    {
        if (!quickSlotMap.ContainsKey(key))
            return;

        quickSlotMap[key] = skillId;
        QuickSlotAssigned?.Invoke(key, skillId);

        Refresh();
    }

    public QuickSlotKey GetEmptyQuickSlotKey()
    {
        foreach (var pair in quickSlotMap)
        {
            if (pair.Value == 0)
                return pair.Key;
        }

        return QuickSlotKey.None;
    }

    public void PressQuickSlotButton(QuickSlotKey key)
    {
        if (quickSlotMap.ContainsKey(key))
            QuickSlotButtonPressed?.Invoke(quickSlotMap[key]);
    }

    private void Refresh()
    {
        quickSlotText.text = string.Empty;
        foreach (var pair in quickSlotMap)
        {
            SkillCommandBase skill = GameManagerWeek5.Instance.player?.skillBook?.GetSkillById(pair.Value);
            if (skill != null)
                quickSlotText.text += $"단축키 {(int)pair.Key}: {skill.skillSO.displayName}\n";
            else
                quickSlotText.text += $"단축키 {(int)pair.Key}: 없음\n";
        }
    }
}