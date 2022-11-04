using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public uint playerLifes { get; set; } = 3;

    AsteroidSpawner asteroidSpawner;

    PlayerController player;
    int enemies;   
    public int InitialAsteroids { get { return initialAsteroids; } } //Es público porque probablemente otros métodos tengan que acceder a este parámetro inicial para setear sus propios parametros iniciales
    [SerializeField]private int initialAsteroids;
    public int Score { get { return score;} }
    private int score;
    private void Start()
    {        
        asteroidSpawner = FindObjectOfType<AsteroidSpawner>();
        NewRound(InitialAsteroids);
        Application.targetFrameRate = 60;        
        score = 0;        
        Test();
        print(enemies);
    }

    private void Update()
    {
        if (enemies == 0)
        {
            initialAsteroids++;
            NewRound(InitialAsteroids);
        }
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }

    public void NewRound(int asteroids)
    {
        enemies = asteroids;
        AsteroidsSpawn(asteroids);
    }

    public void CheckNumberOfEnemies()
    {
        enemies = FindObjectsOfType<EnemyController>().Length;
    }

    private void Test()
    {        
        print(enemies);
        Invoke("Test", 0.5f);
    }

    public void GameOver()
    {
        player = FindObjectOfType<PlayerController>();
        //TODO Game Over
    }

    private void AsteroidsSpawn(int numberOfAsteroids)
    {
        for (int i = 0; i < numberOfAsteroids; i++)
        {
            asteroidSpawner.SpawnRandomAsteroid();
        }
    }
}
