using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OverFlowScreenScript : MonoBehaviour
{
    private GameManager gameManager;
    private MonsterModel monsterModel;
    public GameObject monsterObject;
    public TextMeshProUGUI overflowDamage;

    private List<MonsterModel> monsterOverflowQueue;

    void Awake()
    {
        gameManager = GameManager.Instance;
    }

    public void StartOverflow(List<MonsterModel> monsterOverflowQueue)
    {
        this.monsterOverflowQueue = monsterOverflowQueue;
        SetMonsterData(monsterOverflowQueue.First());
    }

    public void SetMonsterData(MonsterModel monster)
    {
        monsterModel = monster;

        var s = monsterObject.GetComponent<MonsterScript>();
        s.SetMonsterData(monster);

        overflowDamage.text = $"In this case, your town is going to take {monsterModel.BaseMonster.Attack * 2} and gain 1 Fear.";

        monsterOverflowQueue.Remove(monster);
    }

    public void ConfirmOverflow()
    {
        gameManager.Town.Health -= monsterModel.BaseMonster.Attack * 2;
        gameManager.Town.Mood -= 1;

        if (monsterOverflowQueue.Any())
            SetMonsterData(monsterOverflowQueue.First());
        else
        {
            transform.localPosition = new Vector3(2000f, 0f, 0f);
            gameManager.ChangeGameState(GameManager.GameState.HeroTurn);
        }
    }
}
