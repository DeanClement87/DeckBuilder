using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class AvatarScript : MonoBehaviour, IPointerClickHandler
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

    private HeroModel _heroModel;
    public HeroModel HeroModel
    {
        get { return _heroModel; }
        set 
        { 
            _heroModel = value;

            Sprite heroSprite = Resources.Load<Sprite>(HeroModel.HeroArtAvatar);
            heroImage.sprite = heroSprite;
        }
    }

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (gameManager.gameState != GameManager.GameState.HeroTurn) return;

        if (HeroModel.Dead) return;

        gameManager.SetActiveHero(HeroModel);
    }

    private void Update()
    {
        if (HeroModel == null) return;

        if (HeroModel.Dead)
        {
            var red = Resources.Load<Sprite>("AvatarAssets/RedAvatarBackground");
            var avatarImage = transform.parent.GetComponent<Image>();
            avatarImage.sprite = red;
        }

        health.text = HeroModel.Health.ToString();
        var manaGemFull = Resources.Load<Sprite>("AvatarAssets/ManaGemFull");
        var manaGemEmpty = Resources.Load<Sprite>("AvatarAssets/ManaGemEmpty");

        mana1.sprite = manaGemEmpty;
        mana2.sprite = manaGemEmpty;
        mana3.sprite = manaGemEmpty;
        mana4.sprite = manaGemEmpty;
        mana5.sprite = manaGemEmpty;

        if (HeroModel.Mana > 0 ) mana1.sprite = manaGemFull;
        if (HeroModel.Mana > 1 ) mana2.sprite = manaGemFull;
        if (HeroModel.Mana > 2 ) mana3.sprite = manaGemFull;
        if (HeroModel.Mana > 3 ) mana4.sprite = manaGemFull;
        if (HeroModel.Mana > 4 ) mana5.sprite = manaGemFull;
    }


}
