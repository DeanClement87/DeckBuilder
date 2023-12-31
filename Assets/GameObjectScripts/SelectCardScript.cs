using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCardScript : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
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

                gameManager.gameState = GameManager.GameState.SelectCardHeroTurn;
            }
        }
    }
}
