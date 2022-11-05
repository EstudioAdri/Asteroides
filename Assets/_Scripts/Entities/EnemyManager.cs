using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public Dictionary<int, EnemyController> Enemies { get; set; }

    public static EnemyManager inst;

    [SerializeField] public int enemyCount;

    private void Awake()
    {
        enemyCount = 0;
        inst = this;
        Enemies = new Dictionary<int, EnemyController>();
    }

    public static void Remove(int id)
    {
        try
        {
            inst.Enemies.Remove(id);
        }
        catch
        {
            throw new Exception("ENEMYMANAGER_REMOVE: ID not found");
        }
        inst.enemyCount = inst.Enemies.Count;
    }

    public static void Register(int id, EnemyController enemy)
    {
        if (inst.Enemies.ContainsKey(id))
        {
            throw new Exception("ENEMYMANAGER_REGISTER: ID exists");
        }

        inst.Enemies.Add(id, enemy);
        inst.enemyCount = inst.Enemies.Count;
    }    
}
