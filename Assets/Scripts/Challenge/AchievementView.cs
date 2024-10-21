using System;
using System.Collections.Generic;
using UnityEngine;

public class AchievementView : MonoBehaviour
{
    [SerializeField] private GameObject achievementSlotPrefab;  // 업적 슬롯 프리팹
    [SerializeField] private Transform achievementSlotContent;  // 업적 슬롯이 배치될 부모 객체
    private Dictionary<int, AchievementSlot> achievementSlots = new();

    public void CreateAchievementSlots(AchievementSO[] achievements)
    {
        // achievement 데이터에 따라 슬롯을 생성함
        foreach (var achievement in achievements)
        {
            var slot = Instantiate(achievementSlotPrefab, achievementSlotContent).GetComponent<AchievementSlot>();
            slot.Init(achievement);
            achievementSlots.Add(achievement.threshold, slot);
        }
    }

    public void UnlockAchievement(int threshold)
    {
        // UI 반영 로직
        if (achievementSlots.TryGetValue(threshold, out var slot))
        {
            slot.MarkAsChecked();
        }
    }
}