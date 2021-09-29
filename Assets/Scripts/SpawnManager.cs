using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnManager : MonoBehaviour
{
    private Transform spawnPos;
    public GameObject ammoPrefab;
    private IList taggedPrefabs;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spawnPos = transform.Find("ShootPosition");
        taggedPrefabs = Resources.FindObjectsOfTypeAll(typeof(GameObject)).Cast<GameObject>().Where(g => g.tag == "ShooterAmmo").ToList();
        ammoPrefab = (GameObject)taggedPrefabs[0];
        InvokeRepeating("SpawnAmmo", 1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnAmmo()
    {
        Instantiate(ammoPrefab, spawnPos.position, ammoPrefab.transform.rotation);
    }
}
