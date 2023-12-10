using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MonsterScript : MonoBehaviour
{
    private GameManager gameManager;
    public MonsterModel monsterModel;
    public TextMeshProUGUI monsterHealth;
    public TextMeshProUGUI monsterAttack;
    public Image monsterAttributeImage;

    private Image monsterImage;
    private HeroModel LastAttacker;

    private List<GameObject> debuffs = new List<GameObject>();
    private Dictionary<DebuffEnum, bool> debuffDict = new Dictionary<DebuffEnum, bool>();

    void Awake()
    {
        gameManager = GameManager.Instance;
        monsterImage = GetComponent<Image>();

        debuffDict.Add(DebuffEnum.Marked, false);
        debuffDict.Add(DebuffEnum.Frozen, false);
    }

    public void SetMonsterData(MonsterModel monster)
    {
        monsterModel = monster;

        Sprite monsterSprite = Resources.Load<Sprite>(monsterModel.BaseMonster.Image);
        monsterImage.sprite = monsterSprite;

        //need a better way of doing this to be able to show more than 1 attribute at a time.
        Sprite attributeSprite = null;
        foreach (var attribute in monster.BaseMonster.MonsterAttributes)
        {
            switch (attribute)
            {
                case MonsterAttributeEnum.Loot:
                    attributeSprite = Resources.Load<Sprite>("MonsterArt/MonsterAttributeArt/Loot");
                    break;
            }

        }
        if (attributeSprite != null)
            monsterAttributeImage.sprite = attributeSprite;
        else
            monsterAttributeImage.enabled = false;

    }

    public void ResetDebuffs()
    {
        monsterModel.Distract = 0;
        monsterModel.Marked = 0;
        monsterModel.Stunned = false;
        monsterModel.Frozen = false;

        debuffDict[DebuffEnum.Marked] = false;
        debuffDict[DebuffEnum.Frozen] = false;

        foreach (var debuff in debuffs)
            Destroy(debuff);

        debuffs = new List<GameObject>();
    }

    public void AddDebuff(string debuffName)
    {
        var debuffPrefab = Resources.Load<GameObject>("Debuff");
        var debuffObject = GameObject.Instantiate(debuffPrefab, gameObject.transform.position, Quaternion.identity);
        debuffObject.transform.SetParent(gameObject.transform, true);

        var debuff = Resources.Load<DebuffModel>($"Debuffs/{debuffName}");

        var debuffScript = debuffObject.GetComponent<DebuffScript>();
        debuffScript.SetDebuffData(debuff);

        debuffs.Add(debuffObject);

        OrganiseDebuffs();
    }
    public void OrganiseDebuffs()
    {
        switch (debuffs.Count())
        {

            case 1:
                var pos1 = gameObject.transform.position;
                pos1.y += 90;
                debuffs[0].transform.position = pos1;
                break;
            case 2:
                var pos2 = gameObject.transform.position;
                pos2.y += 90;
                pos2.x -= 25;
                debuffs[0].transform.position = pos2;

                var pos3 = gameObject.transform.position;
                pos3.y += 90;
                pos3.x += 25;
                debuffs[1].transform.position = pos3;
                break;

            case 3:
                var pos4 = gameObject.transform.position;
                pos4.y += 90;
                pos4.x -= 50;
                debuffs[0].transform.position = pos4;

                var pos5 = gameObject.transform.position;
                pos5.y += 90;
                debuffs[1].transform.position = pos5;

                var pos6 = gameObject.transform.position;
                pos6.y += 90;
                pos6.x += 50;
                debuffs[2].transform.position = pos6;
                break;

            case 4:
                // Four debuffs
                // will need to resize them at this point to make them fit.
                Console.WriteLine("Four debuffs");
                break;
        }
    }

    public void Update()
    {
        var attack = monsterModel.BaseMonster.Attack - monsterModel.Distract;
        if (attack < 0) attack = 0;

        monsterHealth.text = monsterModel.CurrentHealth.ToString();
        monsterAttack.text = attack.ToString();

        //MARKED
        if (monsterModel.Marked > 0)
        {
            if (debuffDict[DebuffEnum.Marked] == false)
            {
                AddDebuff("Marked");
                debuffDict[DebuffEnum.Marked] = true;
            }
        }

        //FROZEN
        if (monsterModel.Frozen)
        {
            if (debuffDict[DebuffEnum.Frozen] == false)
            {
                AddDebuff("Frozen");
                debuffDict[DebuffEnum.Frozen] = true;
            }
        }

        //DEAD
        if (monsterModel.CurrentHealth <= 0)
        {
            if (monsterModel.BaseMonster.MonsterAttributes.Contains(MonsterAttributeEnum.Loot))
            { 
                if (gameManager.gameState == GameManager.GameState.HeroTurn) 
                    gameManager.gameState = GameManager.GameState.SelectCardHeroTurn;
                else if (gameManager.gameState == GameManager.GameState.MonsterTurn)
                    gameManager.gameState = GameManager.GameState.SelectCardMonsterTurn;
            }

            gameManager.RemoveMonsterFromGame(gameObject, monsterModel);
        }
    }
}
