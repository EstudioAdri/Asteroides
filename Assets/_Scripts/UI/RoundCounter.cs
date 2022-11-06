using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundCounter : MonoBehaviour
{
    GameManager gameManager;
    TextMeshProUGUI text;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        text.SetText($"Round {gameManager.RoundNumber.ToString()}");
    }
}
