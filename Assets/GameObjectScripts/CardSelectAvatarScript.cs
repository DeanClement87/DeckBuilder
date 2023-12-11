using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CardSelectAvatarScript : MonoBehaviour, IPointerClickHandler
{
    private GameManager gameManager;
    public Image heroImage;

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
        var cardRewardScreen = transform.parent.gameObject;
        var sc = cardRewardScreen.GetComponent<CardRewardScreenScript>();

        if (sc.HeroSelected == true)
            return;

        sc.HeroSelected = true;  

        var border = GetComponent<Image>();
        var blueBorder = Resources.Load<Sprite>("AvatarAssets/BlueAvatarBackground");
        border.sprite = blueBorder;

        _heroModel.ShowCardOptions();
    }

    private void Update()
    {
        if (HeroModel == null) return;

    }
}
