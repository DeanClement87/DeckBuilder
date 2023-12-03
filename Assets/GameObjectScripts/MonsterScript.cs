using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterScript : MonoBehaviour
{
    private GameManager gameManager;

    public MonsterModel MonsterModel { get; set; }
    public TextMeshProUGUI monsterHealth;
    public TextMeshProUGUI monsterAttack;
    public GameObject marked;

    private Image monsterImage;

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

        if (MonsterModel.Marked > 0)
            marked.SetActive(true);
        else 
            marked.SetActive(false);

        if (MonsterModel.CurrentHealth <= 0)
        {
            gameManager.RemoveMonsterFromGame(gameObject, MonsterModel);
        }
    }
}
