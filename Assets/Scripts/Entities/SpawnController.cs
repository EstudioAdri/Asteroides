using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] EnemyController enemyGameObject;
    [SerializeField] PlayerController playerGameObject;

    private void Start()
    {
        // Initializes the player object
        Instantiate(playerGameObject);

        // use initialSpawnOfAsteroids
    }

    private void Update()
    {
        // Check number of asteroids in asteroidList
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AddEnemy();
        }
    }

    public void AddEnemy()
    {
        Instantiate(enemyGameObject);
    }

    void SpawnAsteroid()
    {

    }
}
