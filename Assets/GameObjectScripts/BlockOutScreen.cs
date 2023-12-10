using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockOutScreen : MonoBehaviour
{
    private bool BlockOutScreenLive;
    private GameManager gameManager;
    void Awake()
    {
        gameManager = GameManager.Instance;
        transform.SetAsLastSibling();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (gameManager.gameState == GameManager.GameState.SelectCardHeroTurn || gameManager.gameState == GameManager.GameState.SelectCardMonsterTurn)
        {
            transform.SetAsLastSibling();
            PositionHelper.ChangePositionY(gameObject, 0);
            BlockOutScreenLive = true;
        }
        else
        {
            if (BlockOutScreenLive == true)
            {
                //reset all avatar borders to black
                for (int i = 0; i < 4; i++)
                {
                    var cardSelectAvatar1 = GameObject.Find($"CardSelectAvatar{i + 1}");
                    var avatarBorderImage = cardSelectAvatar1.GetComponent<Image>();
                    var blackBorder = Resources.Load<Sprite>("AvatarAssets/BlackAvatarBackground");

                    avatarBorderImage.sprite = blackBorder;
                }

                BlockOutScreenLive = false;
            }
            PositionHelper.ChangePositionY(gameObject, 1620);
        }

    }
}
