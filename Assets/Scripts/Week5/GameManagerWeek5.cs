using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerWeek5 : Singleton<GameManagerWeek5>
{
    public List<SkillSO> skillDataList;
    public Week5Player player;


    private void Start()
    {
        Init();
    }

    private void Init()
    {
        foreach (SkillSO skillSO in skillDataList)
        {
            player.skillBook.AddSkill(skillSO);
        }
    }

}