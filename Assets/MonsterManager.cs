using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    private static MonsterManager _instance;

    public static MonsterManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MonsterManager>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("MonsterManager");
                    _instance = singletonObject.AddComponent<MonsterManager>();

                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private GameManager gameManager;
    public int monsterCounter;
    public List<MonsterTurnModel> monsterTurnList;
    public MonsterTurnModel monsterTurn;
    public void MonsterExecutorStart()
    {
        gameManager = GameManager.Instance;
        monsterCounter = 0;

        monsterTurnList = new List<MonsterTurnModel>();
        var laneNumber = 1;
        foreach (var monsterLane in gameManager.MonsterLanes)
        {
            int index = 0;
            foreach (var monster in monsterLane.MonsterModels)
            {
                var m = new MonsterTurnModel();
                m.monster = monster;
                m.monsterObject = monsterLane.ObjectsInLane[index];
                m.laneNumber = laneNumber;

                monsterTurnList.Add(m);
                index++;

            }
            laneNumber++;
        }

        MonsterExecutor();
    }
    public void MonsterExecutor()
    {
        if (monsterCounter >= monsterTurnList.Count())
        {
            gameManager.ChangeGameState(GameManager.GameState.EndRound);
            monsterTurn = null;
            return;
        }

        monsterTurn = monsterTurnList[monsterCounter];

        //IF MONSTER STUNNED OR FROZEN SKIP
        if (monsterTurn.monster.Stunned || monsterTurn.monster.Frozen)
        {
            monsterTurn.monster.Stunned = false;
            monsterTurn.monster.Frozen = false;
            monsterCounter += 1;
            MonsterExecutor();
        }

        //IF MONSTER RANGED
        if (monsterTurn.monster.BaseMonster.MonsterAttributes.Contains(MonsterAttributeEnum.Ranged))
        {
            var monsterAttack = monsterTurn.monster.BaseMonster.Attack - monsterTurn.monster.Distract;
            if (monsterAttack <= 0) monsterAttack = 0;

            //SIEGE DAMAGE
            if (monsterTurn.monster.BaseMonster.MonsterAttributes.Contains(MonsterAttributeEnum.Siege))
                monsterAttack = monsterAttack * 2;

                var townObject = GameObject.Find($"Town");

            //MONSTER RANGED ATTACK PARTICLES
            ParticleHelper.PerformParticleSequence(monsterTurn.monster.BaseMonster.ParticleEnum, 
                monsterTurn.monster.BaseMonster.ParticleBehavourEnum,
                townObject,
                monsterTurn.monsterObject);

            //MONSTER ATTACK
            gameManager.Town.Health -= monsterAttack;

            monsterCounter += 1;
            MonsterExecutor();
        }
    }

    public class MonsterTurnModel
    {
        public MonsterModel monster { get; set; }
        public GameObject monsterObject { get; set; }
        public int laneNumber { get; set; }
    }
}
