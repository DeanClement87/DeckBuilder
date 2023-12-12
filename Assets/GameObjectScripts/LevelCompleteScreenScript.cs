using TMPro;
using UnityEngine;

public class LevelCompleteScreenScript : MonoBehaviour
{
    private GameManager gameManager;
    public TextMeshProUGUI levelCompleteInfo;

    void Awake()
    {
        gameManager = GameManager.Instance;
    }

    public void SetLevelCompleteStats()
    {
        levelCompleteInfo.text = $"Congratulations, you have beaten level {gameManager.level}";
    }

    public void ConfirmLevelComplete()
    {
        gameManager.level++;
        gameManager.ChangeGameState(GameManager.GameState.LevelStart);

        transform.localPosition = new Vector3(2000f, 0f, 0f);
    }
}
