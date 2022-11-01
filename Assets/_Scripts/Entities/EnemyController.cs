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
}
