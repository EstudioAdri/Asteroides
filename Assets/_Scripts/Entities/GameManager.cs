using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    PlayerController player;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void GameOver()
    {
        player = FindObjectOfType<PlayerController>();
        if (player.Lives == 0)
        {
            //TODO Game Over
        }
    }

}
