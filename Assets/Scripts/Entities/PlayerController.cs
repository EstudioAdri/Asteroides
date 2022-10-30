using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D PlayerRigidBody2d;
    [SerializeField] Transform laserSpawnPoint;

    [Header("Speeds")]
    [SerializeField] float MultiplierForward = 200.0f;
    [SerializeField] float MultiplierRotation = 1.8f;
    [SerializeField] GameObject ProyectilePrefab;
    [SerializeField] float ProyectileSpeedMomentum = 100;
    [SerializeField] float ProyectileSpeedDirection = 500;

    // Start is called before the first frame update
    void Start()
    {
        PlayerRigidBody2d = GetComponent<Rigidbody2D>();
        Physics2D.gravity = Vector2.zero;
        PlayerRigidBody2d.drag = 3.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            PlayerRigidBody2d.AddRelativeForce(Vector2.up * this.MultiplierForward * Time.fixedDeltaTime);
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
    }
}
