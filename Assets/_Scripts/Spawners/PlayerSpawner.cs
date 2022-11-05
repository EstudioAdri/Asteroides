using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] float respawnTimer;
    [SerializeField] PlayerController playerGameObject;
    [SerializeField] bool spawnAreaBusy;
    PlayerController playerInstance;
    float respawnTimeLeft;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        StartCoroutine(SpawnPlayer());
    }

    private void Update()
    {
        if (respawnTimeLeft <= 0 && !spawnAreaBusy && playerInstance == null && gameManager.PlayerLifes > 0)
        {
            playerInstance = Instantiate(playerGameObject);
            respawnTimeLeft = respawnTimer;
        }
        else if (playerInstance == null)
        {
            respawnTimeLeft -= Time.deltaTime;
        }
    }

    public IEnumerator SpawnPlayer()
    {
        while (gameManager.PlayerLifes > 0)
        {

            yield return new WaitForSeconds(respawnTimer);
        }
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