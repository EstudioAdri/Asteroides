using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ProyectileController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 vector;
    [SerializeField] float LifeSpan = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("DestroyThis", LifeSpan);
    }

    void DestroyThis()
    {
        Destroy(this.gameObject);
    }

    public ProyectileController(Vector2 objectPointer, Vector2 objectDirection)
    {
        
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
