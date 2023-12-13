using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TownTargetScript : MonoBehaviour, IPointerClickHandler
{
    private GameManager gameManager;
    private MonsterManager monsterManager;

    void Awake()
    {
        gameManager = GameManager.Instance;
        monsterManager = MonsterManager.Instance;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (gameManager.gameState == GameManager.GameState.MonsterTurn)
        {
            var monsterAttack = monsterManager.monsterTurn.monster.BaseMonster.Attack - monsterManager.monsterTurn.monster.Distract;
            if (monsterAttack <= 0) monsterAttack = 0;

            //SIEGE DAMAGE
            if (monsterManager.monsterTurn.monster.BaseMonster.MonsterAttributes.Contains(MonsterAttributeEnum.Siege))
                monsterAttack = monsterAttack * 2;

            //MONSTER ATTACK
            gameManager.Town.Health -= monsterAttack;

            monsterManager.monsterCounter += 1;
            monsterManager.MonsterExecutor();
        }
    }
}
