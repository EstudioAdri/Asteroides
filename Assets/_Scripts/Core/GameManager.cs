using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    AsteroidSpawner asteroidSpawner;
    PlayerController player;
    public int NumberOfAsteroids { get { return numberOfAsteroids; } } //Es público porque probablemente otros métodos tengan que acceder a este parámetro inicial para setear sus propios parametros iniciales
    [SerializeField]private int numberOfAsteroids;
    private void Start()
    {
        asteroidSpawner = FindObjectOfType<AsteroidSpawner>();
        Application.targetFrameRate = 60;
        AsteroidsSpawn(numberOfAsteroids);
    }

    public void GameOver()
    {
        player = FindObjectOfType<PlayerController>();
        if (player.Lives == 0)
        {
            //TODO Game Over
        }
    }

    private void AsteroidsSpawn(int numberOfAsteroids)
    {
        for (int i = 0; i < numberOfAsteroids; i++)
        {
            asteroidSpawner.SpawnRandomAsteroid();
        }
    }

}
