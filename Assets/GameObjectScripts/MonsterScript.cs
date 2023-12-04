using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MonsterScript : MonoBehaviour
{
    private GameManager gameManager;

    public MonsterModel MonsterModel { get; set; }
    public TextMeshProUGUI monsterHealth;
    public TextMeshProUGUI monsterAttack;

    private Image monsterImage;

    private List<GameObject> debuffs = new List<GameObject>();
    private bool hasBeenMarked = false;

    void Awake()
    {
        gameManager = GameManager.Instance;
        monsterImage = GetComponent<Image>();
    }

    public void SetMonsterData(MonsterModel monster)
    {
        MonsterModel = monster;

        Sprite monsterSprite = Resources.Load<Sprite>(MonsterModel.BaseMonster.Image);
        monsterImage.sprite = monsterSprite;
    }

    public void Update()
    {
        var attack = MonsterModel.BaseMonster.Attack - MonsterModel.Distract;
        if (attack < 0) attack = 0;


        monsterHealth.text = MonsterModel.CurrentHealth.ToString();
        monsterAttack.text = attack.ToString();

        if (MonsterModel.Marked > 0 && hasBeenMarked == false)
        {
            hasBeenMarked = true;
            var debuffPrefab = Resources.Load<GameObject>("Debuff");
            var debuffObject = GameObject.Instantiate(debuffPrefab, gameObject.transform.position, Quaternion.identity);
            debuffObject.transform.SetParent(gameObject.transform, true);

            var debuffPosition = debuffObject.transform.position;
            debuffPosition.y += 90;
            debuffObject.transform.position = debuffPosition;

            //MonsterScript ms = monsterObject.GetComponent<MonsterScript>();
            //ms.SetMonsterData(Monsters[randomIndex]);

            //AddMonsterToLane(monsterObject, Monsters[randomIndex], lane.laneNumber);
        }


        if (MonsterModel.CurrentHealth <= 0)
        {
            gameManager.RemoveMonsterFromGame(gameObject, MonsterModel);
        }
    }
}
