using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int Lives { get { return lives; } }
    public bool RespawnInmunity { get { return respawnImmunity; } }

    [Header("Stats")]
    [SerializeField] int lives;
    [SerializeField] uint totalAvailableShots;
    [SerializeField] float respawnImmunityTimer;
    [SerializeField] float reloadTime;

    [Header("Laser")]
    [SerializeField] Transform laserSpawnPoint;
    [SerializeField] GameObject ProyectilePrefab;
    [SerializeField] float ProyectileSpeedMomentum;
    [SerializeField] float ProyectileSpeedDirection;

    [Header("Speeds")]
    [SerializeField] float MultiplierForward;
    [SerializeField] float MultiplierRotation;

    Rigidbody2D PlayerRigidBody2d;
    GameManager gameManager;
    LifeCounter LifeCounter;

    uint availableShots;
    bool respawnImmunity;


    void Start()
    {
        transform.position = Vector3.zero;
        gameManager = FindObjectOfType<GameManager>();
        PlayerRigidBody2d = GetComponent<Rigidbody2D>();
        LifeCounter = FindObjectOfType<LifeCounter>();

        StartCoroutine(ImmunityTimer());
        ReloadShots();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            PlayerRigidBody2d.AddRelativeForce(Vector2.up * MultiplierForward);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            PlayerRigidBody2d.rotation += MultiplierRotation;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            PlayerRigidBody2d.rotation += -MultiplierRotation;
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.J)) // J used for DEBUG, TO DO add a debug option to toggle this
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Space)) 
        {
            if (availableShots > 0)
            {
                if (availableShots == totalAvailableShots)
                {
                    StartCoroutine(ShotReplenishTimer());
                }
                availableShots --;
                Shoot();
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            float buffer = 50;
            Vector3 worldMin = Camera.main.ScreenToWorldPoint(new Vector2(buffer, buffer));
            Vector2 worldMax = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width - buffer, Screen.height - buffer));
            Vector2 spawnPosition = new Vector2(Random.Range(worldMin.x, worldMax.x), Random.Range(worldMin.y, worldMax.y));
            this.transform.position = spawnPosition;
        }
    }

    IEnumerator ShotReplenishTimer()
    {
        yield return new WaitForSeconds(reloadTime);
        ReloadShots();
    }

    void Shoot()
    {
        float direction = transform.localEulerAngles.z;
        GameObject Proyectile = Instantiate(ProyectilePrefab, gameManager.transform);
        Proyectile.transform.position = laserSpawnPoint.position;
        Proyectile.transform.rotation = transform.rotation;
        Rigidbody2D ProyectileRigidBody2D = Proyectile.GetComponent<Rigidbody2D>();

        Vector2 PlayerDirection;
        float axis;
        switch (direction)
        {
            case <= 45:
                PlayerDirection = new Vector2(-(direction / 45), 1);
                break;
            case <= 90:
                axis = 90 - direction;
                PlayerDirection = new Vector2(-1, (axis / 45));
                break;
            case <= 135:
                axis = 90 - direction;
                PlayerDirection = new Vector2(-1, (axis / 45));
                break;
            case <= 180:
                axis = 180 - direction;
                PlayerDirection = new Vector2(-(axis / 45), -1);
                break;
            case <= 225:
                axis = direction - 180;
                PlayerDirection = new Vector2((axis / 45), -1);
                break;
            case <= 270:
                axis = 270 - direction;
                PlayerDirection = new Vector2(1, -(axis / 45));
                break;
            case <= 315:
                axis = 270 - direction;
                PlayerDirection = new Vector2(1, -(axis / 45));
                break;
            case <= 360:
                axis = 360 - direction;
                PlayerDirection = new Vector2((axis / 45), 1);
                break;
            default:
                PlayerDirection = Vector2.zero;
                break;
        }

        PlayerDirection *= ProyectileSpeedDirection;
        Vector2 PlayerMomentum = PlayerRigidBody2d.velocity * ProyectileSpeedMomentum;
        ProyectileRigidBody2D.AddForce(PlayerDirection + PlayerMomentum);
    }
    
    void ReloadShots()
    {
        availableShots = totalAvailableShots;
    }

    void PlayerDeath()
    {
        gameManager.PlayerLifes--;
        LifeCounter.LoseLife();
        if (gameManager.PlayerLifes == 0)
        {
            print("Player death");
            gameManager.GameOver();
        }
        Destroy(gameObject);

    }

    IEnumerator ImmunityTimer()
    {
        respawnImmunity = true;
        yield return new WaitForSeconds(respawnImmunityTimer);
        respawnImmunity = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedWith = collision.gameObject;
        Vector2 teleport;
        switch (collidedWith.tag)
        {
            case "Enemy":
                if (!respawnImmunity)
                {
                    PlayerDeath();
                }
                break;
            case "Wall X":
                teleport = transform.position;
                teleport.x *= -1;
                if (teleport.x < 0)
                {
                    teleport.x += 0.15f;
                }
                else
                {
                    teleport.x -= 0.15f;
                }
                transform.position = teleport;
                break;
            case "Wall Y":
                teleport = transform.position;
                teleport.y *= -1;
                if (teleport.y < 0)
                {
                    teleport.y += 0.15f;
                }
                else
                {
                    teleport.y -= 0.15f;
                }
                transform.position = teleport;
                break;
        }


    }
}
