using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnManager : MonoBehaviour
{
    private Transform spawnPos;
    private GameManager gameManager;
    public int shooterLevel;

    private int under4EnemiesDelay = 3;
    private int over4EnemiesDelay = 6;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spawnPos = transform.Find("ShootPosition");
        InvokeRepeating(nameof(SpawnAmmoOver4), 1, over4EnemiesDelay);
        InvokeRepeating(nameof(SpawnAmmoUnder4), 1, under4EnemiesDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnAmmoUnder4()
    {
        if (gameManager.enemiesToKill < 4 && gameManager.isAlive && shooterLevel == gameManager.levelNumber && !gameManager.isPaused)
        {
            Instantiate(gameManager.enemyAmmoPrefab, spawnPos.position, gameManager.enemyAmmoPrefab.transform.rotation);
        }
    }

    void SpawnAmmoOver4()
    {
        if (gameManager.enemiesToKill > 4 && gameManager.isAlive && shooterLevel == gameManager.levelNumber && !gameManager.isPaused)
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
