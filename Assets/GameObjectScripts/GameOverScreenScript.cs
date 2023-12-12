using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreenScript : MonoBehaviour
{
    private GameManager gameManager;
    public TextMeshProUGUI gameOverInfo;

    void Awake()
    {
        gameManager = GameManager.Instance;
    }

    public void SetGameOverStats()
    {
        gameOverInfo.text = $"World: {gameManager.world}\n" +
                    $"Level: {gameManager.level}\n" +
                    $"Wave: {gameManager.WaveCounter}";
    }

    public void ConfirmGameOver()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
