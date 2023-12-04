using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterModel
{
    public int CurrentHealth { get; set; }

    //DEBUFFS
    public int Distract { get; set; }
    public int Marked { get; set; }
    public bool Stunned { get; set; }
    public BaseMonsterModel BaseMonster { get; set; }

    public MonsterModel(BaseMonsterModel baseMonster)
    {
        BaseMonster = baseMonster;
        CurrentHealth = baseMonster.Health;
    }
}
