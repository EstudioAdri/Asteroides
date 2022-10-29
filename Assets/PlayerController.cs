using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    private Rigidbody2D PlayerRigidBody2d;
    float MultiplierForward;
    float MultiplierRotation;

    // Start is called before the first frame update
    void Start()
    {
        PlayerRigidBody2d = GetComponent<Rigidbody2D>();
        Physics2D.gravity = Vector2.zero;
        PlayerRigidBody2d.drag = 3.0f;
        MultiplierForward = 200.0f;
        MultiplierRotation = 1.8f;
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
}
