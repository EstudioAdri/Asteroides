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
        // If we press Q we spawn a random asteroid
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SpawnRandomAsteroid();
        }
    }

    public void SpawnRandomAsteroid()
    {
        Vector2 topRightCorner = new Vector2(1, 1);
        Vector2 topEdgeVector = Camera.main.ViewportToWorldPoint(topRightCorner);

        int place = Mathf.RoundToInt(Random.Range(0, 4));
        Vector3 newCoordinates;

        switch(place)
        {
            case 1:
                newCoordinates = new Vector3(Random.Range(-topEdgeVector.x, topEdgeVector.x), topEdgeVector.y);
                break;
            case 2:
                newCoordinates = new Vector3(topEdgeVector.x, Random.Range(-topEdgeVector.y, topEdgeVector.y));
                break;
            case 3:
                newCoordinates = new Vector3(Random.Range(-topEdgeVector.x, topEdgeVector.x), -topEdgeVector.y);
                break;
            case 4:
                newCoordinates = new Vector3(-topEdgeVector.x, Random.Range(-topEdgeVector.y, topEdgeVector.y));
                break;
            default:
                return;
        }

        SpawnAsteroid(newCoordinates);
    }

    public void SpawnAsteroid(Vector3 position)
    {
        Instantiate(enemyGameObject).transform.position = position;
    }
}
