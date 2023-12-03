using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TownScript : MonoBehaviour
{
    private GameManager gameManager;
    public TextMeshProUGUI townHealth;
    public TextMeshProUGUI mood;

    void Awake()
    {
        gameManager = GameManager.Instance;
    }

    void Update()
    {
        //cannot have more health than the starting health
        if (gameManager.Town.Health > gameManager.Town.BaseHealth)
            gameManager.Town.Health = gameManager.Town.BaseHealth;

        townHealth.text = $"HP: {gameManager.Town.Health}";
        mood.text = $"MD: {gameManager.Town.Mood}";
    }
}
