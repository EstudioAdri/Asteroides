using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    [Header("Enemy base stats")]
    [SerializeField] uint health;
    [SerializeField] uint size;
    [SerializeField] float speed;

    private void Awake()
    {
        LoadSpriteAndCollision();
    }

    private void LoadSpriteAndCollision()
    {
        Sprite[] spriteArray = Resources.LoadAll<Sprite>("Sprites/Asteroids/");
        int spriteSelected = Random.Range(0, spriteArray.Length);

        GetComponent<SpriteRenderer>().sprite = spriteArray[spriteSelected];

        gameObject.AddComponent<PolygonCollider2D>();
        GetComponent<PolygonCollider2D>().isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {

    }

    void OnDamage(uint ammount)
    {
        health -= ammount;
        if (health == 0)
        {
            Destroy(this.gameObject);
        }
    }
    void OnDestroy()
    {
        if (health == 0)
        {
            var spawnController = FindObjectOfType<SpawnController>();
            spawnController.SpawnAsteroid(transform.position);
            spawnController.SpawnAsteroid(transform.position);
        }
    }
}
