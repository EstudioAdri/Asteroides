using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int Score { get { return score;} }
    public int RoundNumber { get { return roundNumber;} }
    public uint PlayerLifes { get; set; } = 3;
    
    [SerializeField] int roundNumber;
    [SerializeField] GameObject gameOverCanvasObject;

    EnemyManager asteroidList;
    AsteroidSpawner asteroidSpawner;

    bool gameIsOver;
    int score;

    private void Start()
    {        
        Application.targetFrameRate = 60;

        asteroidList = FindObjectOfType<EnemyManager>();
        asteroidSpawner = FindObjectOfType<AsteroidSpawner>();

        score = 0;
        roundNumber = 0;
    }

    void Update() // TO DO has to be called only when an enemy dies
    {
        if (!gameIsOver && asteroidList.enemyCount == 0)
        {
            roundNumber++;
            asteroidSpawner.NewRoundAsteroidSpawn();
        }
    }

    public void PlusScore(AsteroidStage stage)
    {
        int scoreToAdd = 0;
        switch (stage)
        {
            case AsteroidStage.Big:
                scoreToAdd = 20;
                break;
            case AsteroidStage.Medium:
                scoreToAdd = 50;
                break;
            case AsteroidStage.Small:
                scoreToAdd = 100;
                break;
        }

        score += scoreToAdd;
    }

    public void GameOver()
    {
        gameIsOver = true;
        gameOverCanvasObject.SetActive(true);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
