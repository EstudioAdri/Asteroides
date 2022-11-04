using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public uint playerLifes { get; set; } = 3;

    PlayerController player;
    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    public void GameOver()
    {
        player = FindObjectOfType<PlayerController>();
        //TODO Game Over
    }
}
