using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndHeroTurnScript : MonoBehaviour
{
    private GameManager gameManager;

    void Awake()
    {
        gameManager = GameManager.Instance;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject != gameObject)
                    return;

                gameManager.HideGameObjectOffScreen("EndHeroTurn", true);
                gameManager.ChangeGameState(GameManager.GameState.MonsterTurn);
            }
        }
    }
}
