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
        transform.localScale = new Vector3(0.3f, 0.3f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InstantiateIcon()
    {
        GameObject newObject = Instantiate(ShipIcon, transform);
    }

    public void PrintLifeCounter()
    {
        for (int i = 0; i < gameManager.PlayerLifes; i++)
            InstantiateIcon();
    }

    public void LoseLife()
    {
        Object.Destroy(transform.GetChild(transform.childCount - 1).gameObject);
    }
}
