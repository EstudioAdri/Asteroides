using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCounter : MonoBehaviour
{
    [SerializeField] GameObject ShipIcon;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        PrintLifeCounter();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PrintLifeCounter()
    {
        for (int i = 0; i < gameManager.PlayerLifes; i++)
            Instantiate(ShipIcon, transform);
    }

    public void LoseLife()
    {
        Object.Destroy(transform.GetChild(transform.childCount - 1).gameObject);
    }

    public void GetLife()
    {
        Instantiate(ShipIcon, transform);
    }
}
