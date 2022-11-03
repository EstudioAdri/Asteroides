using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D PlayerRigidBody2d;
    private GameManager gameManager;
    public int Lives { get { return lives; } }
    [Header("Stats")]
    [SerializeField] int lives;
    [SerializeField] float respawnImmunityTimer;
    bool respawnImmunity;

    [Header("Laser")]
    [SerializeField] Transform laserSpawnPoint;
    [SerializeField] GameObject ProyectilePrefab;
    [SerializeField] float ProyectileSpeedMomentum = 100;
    [SerializeField] float ProyectileSpeedDirection = 500;

    [Header("Speeds")]
    [SerializeField] float MultiplierForward = 200.0f;
    [SerializeField] float MultiplierRotation = 1.8f;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        PlayerRigidBody2d = GetComponent<Rigidbody2D>();
        PlayerRigidBody2d.drag = 3.0f;
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            PlayerRigidBody2d.AddRelativeForce(Vector2.up * this.MultiplierForward);
        }

        if (Input.GetKey(KeyCode.A))
        {
            PlayerRigidBody2d.rotation += MultiplierRotation;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            PlayerRigidBody2d.rotation += -MultiplierRotation;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Space)) // me rompe los huevos usar la J ;) - Porcel
        {
            float direction = this.gameObject.transform.localEulerAngles.z;
            GameObject Proyectile = Instantiate<GameObject>(ProyectilePrefab);
            Proyectile.transform.position = laserSpawnPoint.position;
            Proyectile.transform.rotation = this.transform.rotation;
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

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            float buffer = 50;
            Vector3 worldMin = Camera.main.ScreenToWorldPoint(new Vector2(buffer, buffer));
            Vector2 worldMax = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width - buffer, Screen.height -buffer));
            Vector2 spawnPosition = new Vector2(Random.Range(worldMin.x, worldMax.x), Random.Range(worldMin.y, worldMax.y));
            this.transform.position = spawnPosition;
        }

    }

    void PlayerDeath()
    {
        print("Player was hit, respawning");
        // Take away 1 life
        StartCoroutine(ImmunityTimer());
        transform.position = Vector3.zero;
        lives--;
        if (lives == 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {        
        gameManager.GameOver();
    }

    IEnumerator ImmunityTimer()
    {
        respawnImmunity = true;
        yield return new WaitForSeconds(1);
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
