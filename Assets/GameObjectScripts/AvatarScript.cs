using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AvatarScript : MonoBehaviour
{
    private GameManager gameManager;
    public int HeroSelectOrder;
    public Image heroImage;
    public TextMeshProUGUI health;

    public Image mana1;
    public Image mana2;
    public Image mana3;
    public Image mana4;
    public Image mana5;

    private HeroModel heroModel;

    private void Awake()
    {
        gameManager = GameManager.Instance;

        heroModel = gameManager.Heroes[HeroSelectOrder - 1];

        Sprite heroSprite = Resources.Load<Sprite>(heroModel.HeroArtAvatar);
        heroImage.sprite = heroSprite;
    }

    public HeroModel GetHeroModel()
    {
        return heroModel;
    }

    private void OnMouseDown()
    {
        if (gameManager.gameState != GameManager.GameState.HeroTurn) return;

        if (heroModel.Dead) return;

        gameManager.SetActiveHero(heroModel);
    }

    private void Update()
    {
        if (heroModel.Dead)
        {
            var red = Resources.Load<Sprite>("AvatarAssets/RedAvatarBackground");
            var avatarImage = transform.parent.GetComponent<Image>();
            avatarImage.sprite = red;
        }

        health.text = heroModel.Health.ToString();
        var manaGemFull = Resources.Load<Sprite>("AvatarAssets/ManaGemFull");
        var manaGemEmpty = Resources.Load<Sprite>("AvatarAssets/ManaGemEmpty");

        mana1.sprite = manaGemEmpty;
        mana2.sprite = manaGemEmpty;
        mana3.sprite = manaGemEmpty;
        mana4.sprite = manaGemEmpty;
        mana5.sprite = manaGemEmpty;

        if (heroModel.Mana > 0 ) mana1.sprite = manaGemFull;
        if (heroModel.Mana > 1 ) mana2.sprite = manaGemFull;
        if (heroModel.Mana > 2 ) mana3.sprite = manaGemFull;
        if (heroModel.Mana > 3 ) mana4.sprite = manaGemFull;
        if (heroModel.Mana > 4 ) mana5.sprite = manaGemFull;
    }
}
