using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] PlayerController playerGameObject;
    [SerializeField] bool spawnAreaBusy;

    private void Start()
    {
        StartCoroutine(FindObjectOfType<PlayerSpawner>().SpawnPlayer());
    }

    public IEnumerator SpawnPlayer()
    {
        while (true)
        {
            if (!spawnAreaBusy)
            {
                Instantiate(playerGameObject).transform.position = Vector3.zero;
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        spawnAreaBusy = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        spawnAreaBusy = false;
    }
}