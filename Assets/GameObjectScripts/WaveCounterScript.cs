using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveCounterScript : MonoBehaviour
{
    private GameManager gameManager;
    public TextMeshProUGUI waveCounter;
    void Awake()
    {
        gameManager = GameManager.Instance;
    }

    void Update()
    {
        int wave;

        if (gameManager.WaveCounter > 7) wave = 7;
        else wave = gameManager.WaveCounter;

        waveCounter.text = $"Wave: {wave}";
    }
}
