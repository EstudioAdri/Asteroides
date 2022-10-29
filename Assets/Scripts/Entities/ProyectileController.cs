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

    // Update is called once per frame
    void Update()
    {
        
    }
}
