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
            foreach (var monster in monsterLane.MonsterModels)
            {
                var m = new MonsterTurnModel();
                m.monster = monster;
                m.laneNumber = laneNumber;

                monsterTurnList.Add(m);

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

        //IF MONSTER STUNNED SKIP
        if (monsterTurn.monster.Stunned)
        {
            monsterTurn.monster.Stunned = false;
            monsterCounter += 1;
            MonsterExecutor();
        }
    }

    public class MonsterTurnModel
    {
        public MonsterModel monster { get; set; }
        public int laneNumber { get; set; }
    }
}
