using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnManager : MonoBehaviour
{
    private Transform spawnPos;
    private IList taggedPrefabs;
    private GameManager gameManager;
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
        if (!gameManager.isAlive)
        {
            Destroy(gameObject);
        }
    }

    void SpawnAmmo()
    {
        if (gameManager.isAlive)
        {
            Instantiate(gameManager.enemyAmmoPrefab, spawnPos.position, gameManager.enemyAmmoPrefab.transform.rotation);
        }
    }
}
