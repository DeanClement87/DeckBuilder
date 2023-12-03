using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterModel
{
    public Guid Id { get; set; }
    public int CurrentHealth { get; set; }
    public int Distract { get; set; }
    public int Marked { get; set; }
    public BaseMonsterModel BaseMonster { get; set; }

    public MonsterModel(BaseMonsterModel baseMonster)
    {
        Id = Guid.NewGuid();
        BaseMonster = baseMonster;
        CurrentHealth = baseMonster.Health;
    }
}
