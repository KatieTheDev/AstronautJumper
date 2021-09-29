using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isAlive = true;
    public int lives = 3;
    public int levelNumber = 1;
    public int score = 0;

    public GameObject enemyAmmoPrefab;

    private Vector3[] spawnLocation;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ChangeScore", 0.25f, 0.25f); // Add 1 to score about every 1/4th of a second
    }

    // Update is called once per frame
    void Update()
    {
        if (lives < 1)
        {
            isAlive = false;
        }
    }

    void ChangeScore(int scoreChange = 1)
    {
        if (isAlive)
        {
            score += scoreChange;
        }
    }

    Vector3 RemoveLife()
    {
        lives--;
        return spawnLocation[levelNumber];
    }
}
