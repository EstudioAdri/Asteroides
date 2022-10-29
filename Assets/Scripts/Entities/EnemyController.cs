using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy base stats")]
    [SerializeField] uint health;
    [SerializeField] uint size;
    [SerializeField] float speed;

    void OnTriggerEnter(Collider other)
    {
        
    }

    void OnDamage(int ammount)
    {
        
    }

    void OnDestroy()
    {
        // Spawn asteroid
    }
}
