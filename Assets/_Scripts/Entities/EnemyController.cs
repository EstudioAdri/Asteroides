using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    public AsteroidStage stage { get; set; }

    [Header("Enemy base stats")]
    [SerializeField] uint health;
    [SerializeField] uint ammountOfFragments;
    [SerializeField] Vector3 baseScale;
    [SerializeField] float speedMin, speedMax;
    
    [Header("Enemy Count")]
    [SerializeField] uint enemyCount;

    GameManager gameManager;
    AudioManager audioManager;
    PlayerController player;

    private void Awake()
    {
        EnemyManager.Register(gameObject.GetInstanceID(), this);
    }

    private void Start()
    {
        LoadSpriteAndCollision();        
        gameManager = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();
        SetScale();
        float speed = Random.Range(speedMin, speedMax);
        float rotationZ = Random.Range(0f, 361f);

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.rotation += rotationZ;
        rb.AddRelativeForce(Vector2.up * speed, 0f);
    }
    private void LoadSpriteAndCollision()
    {
        Sprite[] spriteArray = Resources.LoadAll<Sprite>("Sprites/Asteroids/");
        int spriteSelected = Random.Range(0, spriteArray.Length);

        GetComponent<SpriteRenderer>().sprite = spriteArray[spriteSelected];

        gameObject.AddComponent<PolygonCollider2D>();
        GetComponent<PolygonCollider2D>().isTrigger = true;
    }

    public void SetScale()
    {
        switch (stage)
        {
            case AsteroidStage.Big:
                transform.localScale = baseScale;
                break;
            case AsteroidStage.Medium:
                transform.localScale = baseScale / 2;
                break;
            case AsteroidStage.Small:
                transform.localScale = baseScale / 4;
                break;
            default:
                Debug.Log("No stage was provided upon spawn of Asteroid");
                break;
        }
    }

    void OnDamage(uint ammount)
    {
        health -= ammount;
        if (health != 0)
        {
            audioManager.Play("AsteroidSmallExplosion");
            return;
        }

        audioManager.Play("AsteroidExplosion");
        if (stage != AsteroidStage.Small)
        {
            var spawnController = FindObjectOfType<AsteroidSpawner>();
            for (int i = 0; i < ammountOfFragments; i++)
            {
                if (stage == AsteroidStage.Big)
                {
                    spawnController.SpawnAsteroid(transform.position, AsteroidStage.Medium);
                }
                else
                {
                    spawnController.SpawnAsteroid(transform.position, AsteroidStage.Small);
                }
            }

        }

        Destroy(this.gameObject);
    }

    void OnDestroy()
    {
        gameManager.PlusScore(stage);
        EnemyManager.Remove(gameObject.GetInstanceID());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedWith = collision.gameObject;
        Vector2 teleport;
        switch (collidedWith.tag)
        {
            case "Laser":
                OnDamage(1); // TO DO Damage must be read from the player
                break;
            case "Player":
                if(!FindObjectOfType<PlayerController>().RespawnInmunity)
                {
                    OnDamage(1); // TO DO Damage must be read from the player
                }
                break;
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
