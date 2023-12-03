using TMPro;
using UnityEngine;

public class ActionDataFollowCursor : MonoBehaviour
{
    private GameManager gameManager;
    private ActionManager actionManager;
    private MonsterManager monsterManager;
    public TextMeshProUGUI actionText;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        actionManager = ActionManager.Instance;
        monsterManager = MonsterManager.Instance;
    }

    void Update()
    {
        if (gameManager.gameState == GameManager.GameState.CardAction || gameManager.gameState == GameManager.GameState.MonsterTurn)
        {
            if (gameManager.gameState == GameManager.GameState.CardAction)
                actionText.text = actionManager.ActiveAction.ActionDataText;
            else if (gameManager.gameState == GameManager.GameState.MonsterTurn)
            {
                var monster = monsterManager.monsterTurn.monster;
                var attack = monster.BaseMonster.Attack - monster.Distract;
                if (attack < 0) attack = 0;

                actionText.text = attack + " Damage";
            }


            // Get the current mouse position in screen space
            Vector3 mousePosition = Input.mousePosition;

            // Adjust the Z position to be in front of the camera
            mousePosition.z = Camera.main.nearClipPlane + 10f;
            mousePosition.x = mousePosition.x + 130f;

            // Convert the screen position to world position
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // Set the position of the GameObject to follow the cursor
            transform.position = worldPosition;
        }
        else
        {
            PositionHelper.ChangePositionY(gameObject, -1000);
        }

    }
}