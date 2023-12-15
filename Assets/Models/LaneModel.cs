using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LaneModel
{
    public int laneNumber = 0;

    public LaneModel OppositeLane { get; set; }
    public List<GameObject> ObjectsInLane { get; set; } = new List<GameObject>();
    public List<HeroModel> HeroesModels { get; set; } = new List<HeroModel>();
    public List<MonsterModel> MonsterModels { get; set; } = new List<MonsterModel>();

    public LaneModel(int laneNumber)
    {
        this.laneNumber = laneNumber;
    }

    public bool IsHeroHere(HeroModel heroModel)
    {
        return HeroesModels.Contains(heroModel);
    }

    public bool IsMonsterHere(MonsterModel monsterModel)
    {
        return MonsterModels.Contains(monsterModel);
    }
}
