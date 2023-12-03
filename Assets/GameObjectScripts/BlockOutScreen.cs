using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockOutScreen : MonoBehaviour
{
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
        if (gameManager.gameState == GameManager.GameState.SelectCard)
        {
            transform.SetAsLastSibling();
            PositionHelper.ChangePositionY(gameObject, 0);
        }
        else
            PositionHelper.ChangePositionY(gameObject, 1620);
    }
}
