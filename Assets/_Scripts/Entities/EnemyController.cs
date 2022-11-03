using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    [Header("Enemy base stats")]
    [SerializeField] uint health;
    [SerializeField] uint size;
    [SerializeField] float speedMin, speedMax;
    
    [Header("Enemy Count")]
    [SerializeField] uint enemyCount;

    private void Awake()
    {
        LoadSpriteAndCollision();
    }    

    private void Start()
    {
        float speed = Random.Range(speedMin, speedMax);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        float rotationZ = Random.Range(0f, 361f);
        rb.rotation += rotationZ;
        rb.AddRelativeForce(Vector2.up * speed,0f);
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
            var spawnController = FindObjectOfType<SpawnController>();
            spawnController.SpawnAsteroid(transform.position);
            spawnController.SpawnAsteroid(transform.position);
            Destroy(this.gameObject);
        }
    }
    void OnDestroy()
    {
        EnemyManager.Remove(gameObject.GetInstanceID());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedWith = collision.gameObject;
        Vector2 teleport;
        switch (collidedWith.tag)
        {
            case "Wall X":
                teleport = this.transform.position;
                teleport.x *= -1;
                if (teleport.x < 0)
                {
                    teleport.x += 0.15f;
                }
                else
                {
                    teleport.x -= 0.15f;
                }
                this.transform.position = teleport;
                break;
            case "Wall Y":
                teleport = this.transform.position;
                teleport.y *= -1;
                if (teleport.y < 0)
                {
                    teleport.y += 0.15f;
                }
                else
                {
                    teleport.y -= 0.15f;
                }
                this.transform.position = teleport;
                break;
        }
    }
}
