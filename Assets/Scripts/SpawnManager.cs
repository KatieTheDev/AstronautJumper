using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnManager : MonoBehaviour
{
    private Transform spawnPos;
    private GameManager gameManager;
    public int shooterLevel;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spawnPos = transform.Find("ShootPosition");
        InvokeRepeating("SpawnAmmo", 1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnAmmo()
    {
        if (gameManager.isAlive && shooterLevel == gameManager.levelNumber)
        {
            Instantiate(gameManager.enemyAmmoPrefab, spawnPos.position, gameManager.enemyAmmoPrefab.transform.rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            gameManager.levelKilled++;
        }
    }
}
