using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] EnemyController enemyGameObject;

    [SerializeField] int initialAsteroids;

    private void Update()
    {
        // DEBUG If we press Q we spawn a random asteroid
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SpawnRandomAsteroid();
        }
    }

    public void NewRoundAsteroidSpawn()
    {
        int numberOfAsteroids = initialAsteroids; // TO DO  + round number (from GameManager)
        for (int i = 0; i < numberOfAsteroids; i++)
        {
            SpawnRandomAsteroid();
        }
    }

    public void SpawnRandomAsteroid()
    {
        /*  
         *  *topRightCorner* represents the corner (1, 1) of the screen such as:
         *  
         *                 (0, 1)  
         *   (-1, 1)____________________ (1, 1)
         *          |                  .
         *          a|          (0, 0)  .
         *          |        Cum       .
         *          |                  .
         *  (-1, -1)|  .   .   .   .   . (-1, 1)
         *                 (0, -1)
         *  
         *  And with Camera.main.ViewportToWorldPoint we transform that relative point
         *  into actual coordinates of the screen and take the x's and y's we want for
         *  painting stuff on the screen
         *  
         *  In *topEdgeVector* we store the actual top corner coordinates of the screen
         *  
         */

        Vector2 topRightCorner = new Vector2(1, 1);
        Vector2 topEdgeVector = Camera.main.ViewportToWorldPoint(topRightCorner);

        /*  
         *  Picks a random number from 1, 2, 3, 4 and depending on this number
         *  we paint the asteroid on one of the edges on the screen, with a random x or y
         *  and a fixed other one so it sticks to the edge of the screen.
         */

        int place = Mathf.RoundToInt(Random.Range(1, 4));
        Vector3 newCoordinates;

        switch(place)
        {
            case 1: newCoordinates = new Vector3(Random.Range(-topEdgeVector.x, topEdgeVector.x), topEdgeVector.y);     break;
            case 2: newCoordinates = new Vector3(topEdgeVector.x, Random.Range(-topEdgeVector.y, topEdgeVector.y));     break;
            case 3: newCoordinates = new Vector3(Random.Range(-topEdgeVector.x, topEdgeVector.x), -topEdgeVector.y);    break;
            case 4: newCoordinates = new Vector3(-topEdgeVector.x, Random.Range(-topEdgeVector.y, topEdgeVector.y));    break;
            default: return;
        }

        SpawnAsteroid(newCoordinates);
    }

    public void SpawnAsteroid(Vector3 position, AsteroidStage stage = AsteroidStage.Big)
    {
        EnemyController currentInstance = Instantiate(enemyGameObject, gameObject.transform);
        currentInstance.stage = stage;
        currentInstance.transform.position = position;
    }
}
