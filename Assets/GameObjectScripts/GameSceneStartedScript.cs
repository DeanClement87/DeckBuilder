using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneStartedScript : MonoBehaviour
{
    private GameManager gameManager;
    void Awake()
    {
        gameManager = GameManager.Instance;
        gameManager.ChangeGameState(GameManager.GameState.GameStart);
    }
}
