using System.Collections.Generic;
using UnityEngine;

public class InitMonsterWorld1
{
    private static List<BaseMonsterModel> BaseMonsters { get; set; } = new List<BaseMonsterModel>();

    public static List<MonsterModel> InitMonsters(int level)
    {
        BaseMonsters.Add(Resources.Load<BaseMonsterModel>("MonsterBases/Ratman_01"));
        BaseMonsters.Add(Resources.Load<BaseMonsterModel>("MonsterBases/Ratman_02"));
        BaseMonsters.Add(Resources.Load<BaseMonsterModel>("MonsterBases/Ratman_03"));
        BaseMonsters.Add(Resources.Load<BaseMonsterModel>("MonsterBases/Ratman_04"));
        BaseMonsters.Add(Resources.Load<BaseMonsterModel>("MonsterBases/Ratman_05"));

        switch (level)
        {
            case 1:
                return Level1();
        }

        return new List<MonsterModel>();
    }

    private static List<MonsterModel> Level1()
    {
        var monster1 = new MonsterModel(BaseMonsters[0]);
        var monster2 = new MonsterModel(BaseMonsters[0]);
        var monster3 = new MonsterModel(BaseMonsters[0]);
        var monster4 = new MonsterModel(BaseMonsters[0]);
        var monster5 = new MonsterModel(BaseMonsters[0]);
        var monster6 = new MonsterModel(BaseMonsters[0]);
        var monster7 = new MonsterModel(BaseMonsters[0]);
        var monster8 = new MonsterModel(BaseMonsters[0]);
        var monster9 = new MonsterModel(BaseMonsters[0]);
        var monster10 = new MonsterModel(BaseMonsters[0]);
        var monster11 = new MonsterModel(BaseMonsters[1]);
        var monster12 = new MonsterModel(BaseMonsters[1]);
        var monster13 = new MonsterModel(BaseMonsters[1]);
        var monster14 = new MonsterModel(BaseMonsters[1]);
        var monster15 = new MonsterModel(BaseMonsters[2]);
        var monster16 = new MonsterModel(BaseMonsters[2]);
        var monster17 = new MonsterModel(BaseMonsters[2]);
        var monster18 = new MonsterModel(BaseMonsters[2]);
        var monster19 = new MonsterModel(BaseMonsters[3]);
        var monster20 = new MonsterModel(BaseMonsters[3]);
        var monster21 = new MonsterModel(BaseMonsters[4]);

        var monsters = new List<MonsterModel>();
        monsters.Add(monster1);
        monsters.Add(monster2);
        monsters.Add(monster3);
        monsters.Add(monster4);
        monsters.Add(monster5);
        monsters.Add(monster6);
        monsters.Add(monster7);
        monsters.Add(monster8);
        monsters.Add(monster9);
        monsters.Add(monster10);
        monsters.Add(monster11);
        monsters.Add(monster12);
        monsters.Add(monster13);
        monsters.Add(monster14);
        monsters.Add(monster15);
        monsters.Add(monster16);
        monsters.Add(monster17);
        monsters.Add(monster18);
        monsters.Add(monster19);
        monsters.Add(monster20);
        monsters.Add(monster21);

        return monsters;
    }
}
