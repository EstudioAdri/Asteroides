using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] EnemyController enempyGameObject;
    [SerializeField] PlayerController playerGameObject;

    [Header("Asteroids")]
    [SerializeField] int initialSpawnOfAsteroids;
    [SerializeField] List<EnemyController> asteroidList;

    private void Start()
    {
        Instantiate(playerGameObject);
        // use initialSpawnOfAsteroids
    }

    private void Update()
    {
        // Check number of asteroids in asteroidList
    }

    void SpawnAsteroid()
    {

    }
}
