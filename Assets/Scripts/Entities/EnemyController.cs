using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy base stats")]
    [SerializeField] uint health;
    [SerializeField] uint size;
    [SerializeField] float speed;

    [Header("Enemy Count")]
    [SerializeField] uint enemyCount;

    private void Start()
    {
        EnemyManager.Register(gameObject.GetInstanceID(), this);
    }

    void OnTriggerEnter(Collider other)
    {
        
    }

    void OnDamage(uint ammount)
    {
        health -= ammount;
        if (health == 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnDestroy()
    {
        EnemyManager.Remove(gameObject.GetInstanceID());
    }
}
